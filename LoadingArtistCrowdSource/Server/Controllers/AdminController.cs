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
using Microsoft.EntityFrameworkCore;
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
		private readonly Services.HistoryLogger _historyLogger;

		public AdminController(
			ApplicationDbContext context, 
			IHttpClientFactory httpClientFactory, 
			ILogger<AdminController> logger, 
			UserManager<Models.ApplicationUser> userManager,
			Services.HistoryLogger historyLogger)
		{
			_context = context;
			_httpClient = httpClientFactory.CreateClient();
			_logger = logger;
			_userManager = userManager;
			_historyLogger = historyLogger;
		}

		[HttpPost]
		[Route("import_comics")]
		public async Task<IActionResult> ImportNewComics()
		{
			_logger.LogInformation($"User {User.Identity?.Name} initiated importing new comics.");
			Queue<Models.Comic> importableComics = await GetImportableRssFeedItems();
			int importableComicsCount = importableComics.Count;

			var latestComic = await _context.Comics.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
			int latestComicId = latestComic?.Id ?? 0;
			_logger.LogInformation($"Beginning import. Latest comic id = {latestComicId}");

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					int currentComicId = latestComicId + 1;
					foreach (var comic in importableComics)
					{
						_logger.LogInformation($"Importing comic '{comic.Code}' '{comic.Permalink}' with a new id of '{currentComicId}'");
						comic.Id = currentComicId;
						currentComicId += 1;
						_context.Comics.Add(comic);
						await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateComicImportedLog(comic));
					}

					_logger.LogInformation($"Importing comics completed. Saving changes.");
					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error importing comics");
					await transaction.RollbackAsync();
					throw;
				}
			}
			
			_logger.LogInformation($"Importing and saving comics completed.");
			return Ok($"Imported {importableComicsCount} comic(s)");
		}

		private async Task<SyndicationFeed> GetRssFeedPage(int page)
		{
			string sFeedUrl = "https://loadingartist.com/feed?post_type=comic&paged={0}";

			string url = string.Format(sFeedUrl, page);

			_logger.LogInformation($"Pulling RSS page {page}: {url}");
			SyndicationFeed syndicationFeed;

			Stream responseStream;
			try
			{
				responseStream = await _httpClient.GetStreamAsync(url);
			}
			catch (HttpRequestException ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					_logger.LogInformation($"On page {page}, received 404. Ending.");
					return new SyndicationFeed();
				}
				throw;
			}

			using (var stream = responseStream)
			using (var xmlReader = XmlReader.Create(stream))
			{
				syndicationFeed = SyndicationFeed.Load(xmlReader);
			}
			_logger.LogInformation($"Done loading feed page. Feed contains {syndicationFeed.Items.Count()} items.");

			return syndicationFeed;
		}

		private async Task<Queue<Models.Comic>> GetImportableRssFeedItems()
		{
			var userId = _userManager.GetUserId(User);
			List<Models.Comic> importableItemsNewestFirst = new List<Models.Comic>();

			var latestComic = await _context.Comics.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
			_logger.LogInformation($"Latest comic: {latestComic?.Code} {latestComic?.Permalink}");

			int page = 1;
			bool shouldBreakLoop = false;
			while (!shouldBreakLoop)
			{
				var feedPage = await GetRssFeedPage(page);
				page += 1;
				if (!feedPage.Items.Any())
				{
					break;
				}
				foreach (var feedItem in feedPage.Items)
				{
					var comic = CreateComic(feedItem, id: 0, userId: userId);
					if (latestComic?.Permalink == comic.Permalink)
					{
						_logger.LogInformation($"RSS Comic '{comic.Code}' '{comic.Permalink}' matches latest comic in system. Stopping.");
						shouldBreakLoop = true;
						break;
					}

					_logger.LogInformation($"RSS Comic '{comic.Code}' '{comic.Permalink}' is new. Queueing for insertion.");
					importableItemsNewestFirst.Add(comic);
				}
			}

			return new Queue<Models.Comic>(importableItemsNewestFirst.Reverse<Models.Comic>());
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
