using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[Authorize(Roles = Roles.Administrator)]
	[ApiController]
	[Route("api/admin")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly HttpClient _httpClient;
		private readonly ILogger<AdminController> _logger;
		private readonly UserManager<Models.ApplicationUser> _userManager;

		public AdminController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, ILogger<AdminController> logger, UserManager<Models.ApplicationUser> userManager)
		{
			_context = context;
			_httpClient = httpClientFactory.CreateClient();
			_logger = logger;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("compile_feed")]
		public async Task<IActionResult> CompileFeed()
		{
			string sFeedUrl = "https://loadingartist.com/feed?post_type=comic&paged={0}";
			int nNextPage = 0;

			Func<string> getNextUrl = () => string.Format(sFeedUrl, nNextPage++);

			XmlDocument xdFeed = new XmlDocument();
			try
			{
				var url = getNextUrl();
				_logger.LogInformation($"Downloading {url} ...");
				var sFeedContent = await _httpClient.GetStringAsync(url);
				_logger.LogInformation("Processing...");
				xdFeed.LoadXml(sFeedContent);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"On page {nNextPage-1}");
				return BadRequest();
			}

			var xeFeedChannel = xdFeed.SelectSingleNode("/rss/channel") as XmlElement;
			if (xeFeedChannel == null)
			{
				_logger.LogError("Feed channel was not found");
				return BadRequest();
			}

			int importedNodes = 0;
			while (true)
			{
				XmlDocument xFeedAdditional = new XmlDocument();
				string sFeedAdditionalContent = "";
				try
				{
					var url = getNextUrl();
					_logger.LogInformation($"Downloading {url} ...");
					sFeedAdditionalContent = await _httpClient.GetStringAsync(url);
					_logger.LogInformation("Processing...");
					xFeedAdditional.LoadXml(sFeedAdditionalContent);
				}
				catch (HttpRequestException ex)
				{
					if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
					{
						_logger.LogInformation($"On page {nNextPage - 1}, received 404. Ending.");
						break;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, $"On additional page {nNextPage - 1}");
					return BadRequest();
				}

				var xelFeedItems = xFeedAdditional.SelectNodes("/rss/channel/item");
				if (xelFeedItems == null)
				{
					_logger.LogWarning($"Additional feed items was null, on page {nNextPage - 1}: {sFeedAdditionalContent}");
					break;
				}

				if (xelFeedItems.Count == 0)
				{
					_logger.LogInformation("No channel items found. Ending.");
					break;
				}

				foreach (XmlNode xnItem in xelFeedItems)
				{
					if (xnItem != null)
					{
						var newNode = xdFeed.ImportNode(xnItem, deep: true);
						xeFeedChannel.AppendChild(newNode);
						importedNodes += 1;
					}
				}
			}

			_logger.LogInformation($"Imported {nNextPage} pages with 10+{importedNodes} items.");
			
			return File(System.Text.Encoding.UTF8.GetBytes(xdFeed.OuterXml), "application/xml", "feed.xml");
		}

		[HttpPost]
		[Route("import_feed")]
		public async Task<IActionResult> ImportFeed()
		{
			var userId = _userManager.GetUserId(User);
			var formFile = Request.Form.Files.FirstOrDefault();
			if (formFile == null)
			{
				return BadRequest("No file found");
			}

			if (_context.Comics.Any())
			{
				return BadRequest("Comics already exist");
			}

			SyndicationFeed syndicationFeed;
			using (var stream = formFile.OpenReadStream())
			using (var xmlReader = XmlReader.Create(stream))
			{
				syndicationFeed = SyndicationFeed.Load(xmlReader);
			}

			int id = 1;
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					foreach (var feedItem in syndicationFeed.Items.Reverse())
					{
						id += 1;
						var newComic = CreateComic(feedItem, id, userId);
						_context.Comics.Add(newComic);
					}
					
					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error");
					await transaction.RollbackAsync();
					return BadRequest("Something went wrong");
				}
			}

			return Ok($"Success - {id-1} comics imported");
		}

		private Models.Comic CreateComic(SyndicationItem feedItem, int id, string userId)
		{
			Uri? link = feedItem.Links.FirstOrDefault(l => l.RelationshipType == "alternate")?.Uri;
			if (link == null)
			{
				throw new Exception("Could not find alternate link");
			}

			string? code = link.Segments.LastOrDefault();
			if (string.IsNullOrEmpty(code))
			{
				throw new Exception("Could not find code");
			}
			code = code.TrimEnd('/');

			string? tooltip = feedItem.Summary?.Text;

			string? description = feedItem.ElementExtensions
				.FirstOrDefault(e => e.OuterName == "encoded" && e.OuterNamespace == "http://purl.org/rss/1.0/modules/content/")?
				.GetObject<string?>();
			if (string.IsNullOrEmpty(description))
			{
				throw new Exception("Description is blank");
			}

			var year = $"{feedItem.PublishDate.Year:00}";
			var month = $"{feedItem.PublishDate.Month:00}";
			var day = $"{feedItem.PublishDate.Day:00}";

			// Comics from Born (2011) to High Five (2014-08-15) are PNG,
			// and from Drawn Back (2014-08-22) to current are JPG.
			string imageUrlSrc = $"https://loadingartist.com/wp-content/uploads/{year}/{month}/{year}-{month}-{day}-{code}.jpg";
			if (feedItem.PublishDate < new DateTime(2014, 08, 21))
			{
				imageUrlSrc = $"https://loadingartist.com/wp-content/uploads/{year}/{month}/{year}-{month}-{day}-{code}.png";
			}

			var comic = new Models.Comic()
			{
				Id = id,
				Code = code,
				Permalink = link.OriginalString,
				ComicPublishedDate = feedItem.PublishDate,
				Title = feedItem.Title.Text,
				Tooltip = tooltip,
				Description = description,
				ImageUrlSrc = imageUrlSrc,
				ImageThumbnailUrlSrc = $"https://loadingartist.com/comic-thumbs/{code}.png",
				ImageWideThumbnailUrlSrc = $"https://loadingartist.com/comic-thumbs-wide/{code}.png",
				ImportedDate = DateTimeOffset.Now,
				ImportedBy = userId,
				LastUpdatedDate = null,
				LastUpdatedBy = null,
			};

			return comic;
		}
	}
}
