using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
		private readonly Services.JsonDistributedCache<AdminController> _distCache;
		private readonly IConfiguration _configuration;

		private string LADomain => _configuration.GetValue<string>(Services.ServerConfig.LACS_LoadingArtistDomain);

		public AdminController(
			ApplicationDbContext context, 
			IHttpClientFactory httpClientFactory, 
			ILogger<AdminController> logger, 
			UserManager<Models.ApplicationUser> userManager,
			Services.HistoryLogger historyLogger,
			Services.JsonDistributedCache<AdminController> distCache,
			IConfiguration configuration)
		{
			_context = context;
			_httpClient = httpClientFactory.CreateClient();
			_logger = logger;
			_userManager = userManager;
			_historyLogger = historyLogger;
			_distCache = distCache;
			_configuration = configuration;
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

			await _distCache.RemoveAsync(Services.CacheKeys.LACS.ComicIndex);
			
			_logger.LogInformation($"Importing and saving comics completed.");
			return Ok($"Imported {importableComicsCount} comic(s)");
		}

		[HttpPost]
		[Route("import_initial_comics")]
		public async Task<IActionResult> ImportInitialComics()
		{
			var formFile = Request.Form.Files.FirstOrDefault();
			if (formFile == null)
			{
				return BadRequest("No xml file uploaded.");
			}

			_logger.LogInformation($"User {User.Identity?.Name} initiated importing initial comics.");
			Queue<Models.Comic> importableComics = await GetAllComics(formFile);
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

			await _distCache.RemoveAsync(Services.CacheKeys.LACS.ComicIndex);
			
			_logger.LogInformation($"Importing and saving comics completed.");
			return Ok($"Imported {importableComicsCount} comic(s)");
		}

		[HttpPut]
		[Route("user/{username}/roles")]
		public async Task<IActionResult> PutUserRole([FromRoute] string username, [FromBody] IEnumerable<Shared.Models.AdminSetUserRoleItemViewModel> userRoleItems)
		{
			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
			{
				_logger.LogWarning($"User {username} not found");
				return NotFound("User name not found");
			}

			IEnumerable<string> userRoles = userRoleItems.Where(uri => uri.Include).Select(uri => uri.Role);

			List<string> rolesRemoved = new List<string>();
			List<string> rolesAdded = new List<string>();
			List<string> rolesUnchanged = new List<string>();

			IList<string> currentRoles = await _userManager.GetRolesAsync(user);
			foreach (string role in currentRoles)
			{
				if (!userRoles.Contains(role))
				{
					rolesRemoved.Add(role);
					await _userManager.RemoveFromRoleAsync(user, role);
				}
				else
				{
					rolesUnchanged.Add(role);
				}
			}
			rolesAdded.AddRange(userRoles.Except(currentRoles));
			await _userManager.AddToRolesAsync(user, userRoles);

			_logger.LogWarning($"Removed: {string.Join(", ", rolesRemoved)}; Added: {string.Join(", ", rolesAdded)}; Unchanged: {string.Join(", ", rolesUnchanged)}");
			return Ok($"Removed: {string.Join(", ", rolesRemoved)}; Added: {string.Join(", ", rolesAdded)}; Unchanged: {string.Join(", ", rolesUnchanged)}");
		}

		[HttpGet]
		[Route("export_fields")]
		public async Task<IActionResult> ExportFieldDefinitions()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			List<Models.CrowdSourcedFieldDefinition> fieldDefs = await _context.CrowdSourcedFieldDefinitions
				.Include(csdf => csdf.CreatedByUser)
				.Include(csdf => csdf.LastUpdatedByUser)
				.Include(csdf => csdf.CrowdSourcedFieldDefinitionOptions)
				.OrderBy(csdf => csdf.DisplayOrder)
				.ToListAsync();

			foreach (var fieldDef in fieldDefs)
			{
				fieldDef.CrowdSourcedFieldDefinitionOptions = fieldDef.CrowdSourcedFieldDefinitionOptions
					.OrderBy(csfdo => csfdo.DisplayOrder).ToList();
			}

			List<Shared.Models.FieldDefinitionFormViewModel> fieldVms = fieldDefs
				.Select(fd => modelMapper.MapFieldDefinitionForm(fd, mapOptions: true))
				.ToList();

			var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
			string json = JsonSerializer.Serialize(fieldVms, serializerOptions);

			// return File(Encoding.UTF8.GetBytes(json), "application/json", "LACSFields.json");
			return Content(json);
		}

		[HttpPost]
		[Route("import_fields")]
		public async Task<IActionResult> ImportFieldDefinitions()
		{
			string json;
			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
				json = await reader.ReadToEndAsync();
			}

			var userId = _userManager.GetUserId(User);
			if (await _context.CrowdSourcedFieldDefinitions.AnyAsync())
			{
				return BadRequest("Can not import fields into a site where fields already exist");
			}

			var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
			var lstFieldViewModels = JsonSerializer.Deserialize<List<Shared.Models.FieldDefinitionFormViewModel>>(json, serializerOptions);

			if (lstFieldViewModels == null)
			{
				return BadRequest("Unable to parse import JSON");
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					foreach (var field in lstFieldViewModels.Select((fieldVm, index) => new { fieldVm, index }))
					{
						var fieldVm = field.fieldVm;
						var fieldDef = new Models.CrowdSourcedFieldDefinition()
						{
							Code = fieldVm.Code,
							CreatedDate = DateTimeOffset.Now,
							CreatedBy = userId,
							IsActive = fieldVm.IsActive,
							Type = fieldVm.Type,
							Name = fieldVm.Name,
							ShortDescription = fieldVm.ShortDescription,
							LongDescription = fieldVm.LongDescription,
							DisplayOrder = field.index,
						};
						_context.CrowdSourcedFieldDefinitions.AddRange(fieldDef);
						await _context.CrowdSourcedFieldDefinitionHistoryLogs.AddAsync(_historyLogger.CreateAddFieldDefinitionLog(fieldDef));

						fieldDef.CrowdSourcedFieldDefinitionOptions.AddRange(fieldVm.Options.Select((optionVM, index) => new Models.CrowdSourcedFieldDefinitionOption()
						{
							Code = optionVM.Code,
							Text = optionVM.Text,
							Description = optionVM.Description,
							URL = optionVM.URL,
							DisplayOrder = index,
						}));

						// Save per field to get the correct order.
						await _context.SaveChangesAsync();
					}

					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error importing fields");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear all comics from cache
			foreach (string comicCode in _context.Comics.Select(c => c.Code))
			{
				await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));
			}

			return Ok($"Imported {lstFieldViewModels.Count} fields");
		}

		[HttpPost]
		[Route("comic/{comicCode}/delete_all_field_data")]
		public async Task<IActionResult> DeleteAllComicFieldData([FromRoute] string comicCode)
		{
			var comic = _context.Comics.FirstOrDefault(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound();
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					var csfvevs = await _context.CrowdSourcedFieldVerifiedEntryValues
						.Where(csfvev => csfvev.ComicId == comic.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldVerifiedEntryValues.RemoveRange(csfvevs);

					var csfves = await _context.CrowdSourcedFieldVerifiedEntries
						.Where(csfve => csfve.ComicId == comic.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldVerifiedEntries.RemoveRange(csfves);

					var csfuevs = await _context.CrowdSourcedFieldUserEntryValues
						.Where(csfuev => csfuev.ComicId == comic.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldUserEntryValues.RemoveRange(csfuevs);

					var csfues = await _context.CrowdSourcedFieldUserEntries
						.Where(csfue => csfue.ComicId == comic.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldUserEntries.RemoveRange(csfues);

					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error importing fields");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear cache for this comic
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));

			return Ok();
		}

		[HttpPost]
		[Route("field/{fieldCode}/delete_all_field_data")]
		public async Task<IActionResult> DeleteAllFieldDefinitionData([FromRoute] string fieldCode)
		{
			var fieldDef = _context.CrowdSourcedFieldDefinitions.FirstOrDefault(csfd => csfd.Code == fieldCode);
			if (fieldDef == null)
			{
				return NotFound();
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					var csfvevs = await _context.CrowdSourcedFieldVerifiedEntryValues
						.Where(csfvev => csfvev.CrowdSourcedFieldDefinitionId == fieldDef.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldVerifiedEntryValues.RemoveRange(csfvevs);

					var csfves = await _context.CrowdSourcedFieldVerifiedEntries
						.Where(csfve => csfve.CrowdSourcedFieldDefinitionId == fieldDef.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldVerifiedEntries.RemoveRange(csfves);

					var csfuevs = await _context.CrowdSourcedFieldUserEntryValues
						.Where(csfuev => csfuev.CrowdSourcedFieldDefinitionId == fieldDef.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldUserEntryValues.RemoveRange(csfuevs);

					var csfues = await _context.CrowdSourcedFieldUserEntries
						.Where(csfue => csfue.CrowdSourcedFieldDefinitionId == fieldDef.Id)
						.ToListAsync();
					_context.CrowdSourcedFieldUserEntries.RemoveRange(csfues);

					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error importing fields");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear all comics from cache
			foreach (string comicCode in _context.Comics.Select(c => c.Code))
			{
				await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));
			}

			return Ok();
		}

		#region Private Methods
		private async Task<SyndicationFeed> GetRssFeedPage(int page)
		{
			string sFeedUrl = $"https://{LADomain}/feed?post_type=comic&paged={0}";

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
					var comic = await CreateComic(feedItem, id: 0, userId: userId);
					if (latestComic?.Permalink == comic.Permalink)
					{
						_logger.LogInformation($"RSS Comic '{comic.Code}' '{comic.Permalink}' matches latest comic in system. Stopping.");
						shouldBreakLoop = true;
						break;
					}

					_logger.LogInformation($"RSS Comic '{comic.Code}' '{comic.Permalink}' is new. Queueing for insertion.");
					importableItemsNewestFirst.Add(comic);
				}

				// The old wordpress website supported paginated RSS feed. The new website does not.
				// So, as a simple fix for now, always break here to never attempt to get the next page.
				break;
			}

			return new Queue<Models.Comic>(importableItemsNewestFirst.Reverse<Models.Comic>());
		}

		private async Task<Queue<Models.Comic>> GetAllComics(IFormFile file)
		{
			SyndicationFeed syndicationFeed;

			using (var stream = file.OpenReadStream())
			using (var xmlReader = XmlReader.Create(stream))
			{
				syndicationFeed = SyndicationFeed.Load(xmlReader);
			}

			List<Models.Comic> importableItemsNewestFirst = new List<Models.Comic>();
			var userId = _userManager.GetUserId(User);

			var latestComic = await _context.Comics.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
			_logger.LogInformation($"Latest comic: {latestComic?.Code} {latestComic?.Permalink}");

			foreach (var feedItem in syndicationFeed.Items)
			{
				if (feedItem.Categories.FirstOrDefault()?.Name != "comic")
				{
					continue;
				}

				var comic = await CreateComic(feedItem, id: 0, userId: userId);
				if (latestComic?.Permalink == comic.Permalink)
				{
					_logger.LogInformation($"RSS Comic '{comic.Code}' '{comic.Permalink}' matches latest comic in system. Stopping.");
					break;
				}

				_logger.LogInformation($"RSS Comic '{comic.Code}' '{comic.Permalink}' is new. Queueing for insertion.");
				importableItemsNewestFirst.Add(comic);
			}

			return new Queue<Models.Comic>(importableItemsNewestFirst.Reverse<Models.Comic>());
		}

		private async Task<Models.Comic> CreateComic(SyndicationItem feedItem, int id, string userId)
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

			var asContext = AngleSharp.BrowsingContext.New(AngleSharp.Configuration.Default);
			var document = await AngleSharp.BrowsingContextExtensions.OpenAsync(asContext, req => req.Content(description));
			var sourceSrc = document.QuerySelector("picture>source")?.GetAttribute("src");
			var imgSrc = document.QuerySelector("picture>img")?.GetAttribute("src");
			string imageUrlSrc = sourceSrc ?? imgSrc ?? $"https://{LADomain}/comic/{code}/{code}.jpg";

			// Attempt to use webp over gif.
			if (imageUrlSrc.EndsWith(".gif"))
			{
				imageUrlSrc = imageUrlSrc.Replace(".gif", ".webp");
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
				ImageThumbnailUrlSrc = $"https://{LADomain}/comic/{code}/thumb.png",
				ImageWideThumbnailUrlSrc = $"https://{LADomain}/comic/{code}/thumb_wide.png",
				ImportedDate = DateTimeOffset.Now,
				ImportedBy = userId,
				LastUpdatedDate = null,
				LastUpdatedBy = null,
			};

			return comic;
		}
		#endregion Private Methods
	}
}
