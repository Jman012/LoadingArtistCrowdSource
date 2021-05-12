using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[ApiController]
	[Route("api/comic")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class ComicController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Models.ApplicationUser> _userManager;
		private readonly ILogger<ComicController> _logger;
		private readonly Services.IRazorPartialToStringRenderer _renderer;
		private readonly Services.HistoryLogger _historyLogger;
		private readonly Services.TagRepository _tagRepo;
		private readonly Services.JsonDistributedCache<ComicController> _distCache;

		public ComicController(
			ApplicationDbContext context, 
			UserManager<Models.ApplicationUser> userManager, 
			ILogger<ComicController> logger,
			Services.IRazorPartialToStringRenderer renderer,
			Services.HistoryLogger historyLogger,
			Services.TagRepository tagRepo,
			Services.JsonDistributedCache<ComicController> distCache)
		{
			_context = context;
			_userManager = userManager;
			_logger = logger;
			_renderer = renderer;
			_historyLogger = historyLogger;
			_tagRepo = tagRepo;
			_distCache = distCache;
		}

		[HttpGet]
		public async Task<IEnumerable<Shared.Models.ComicListItemViewModel>> Index()
		{
			return await _distCache.GetAsync(Services.CacheKeys.LACS.ComicIndex, async () => {
				Services.ModelMapper modelMapper = new Services.ModelMapper();
				var comics = await _context.Comics
					.OrderBy(c => c.Id)
					.ToListAsync();

				var cacheEntryOptions = new DistributedCacheEntryOptions()
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4),
				};

				return (comics.Select(c => modelMapper.MapComicListItem(c)), cacheEntryOptions);
			});
		}

		[HttpGet]
		[Route("{code}")]
		public async Task<IActionResult> GetComic(string code)
		{
			// Attempt to fulfill request from cache
			var result = await _distCache.GetAsync<Shared.Models.ComicPageViewModel>(Services.CacheKeys.LACS.GetComic(code));
			if (result != null)
			{
				return Json(result);
			}

			// Retrieve comic from storage
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			Models.Comic? comic = await _context.Comics
				.Include(c => c.ImportedByUser)
				.Include(c => c.LastUpdatedByUser)
				.Include(c => c.ComicTags).ThenInclude(ct => ct.CreatedByUser)
				.Include(c => c.ComicTranscript).ThenInclude(ct => ct!.LastEditedByUser)
				.Include(c => c.CrowdSourcedFieldVerifiedEntries).ThenInclude(ve => ve.CrowdSourcedFieldVerifiedEntryValues)
				.Include(c => c.CrowdSourcedFieldUserEntries).ThenInclude(ue => ue.CrowdSourcedFieldUserEntryValues)
				.Include(c => c.CrowdSourcedFieldUserEntries).ThenInclude(ue => ue.CreatedByUser)
				.FirstOrDefaultAsync(c => c.Code == code);

			if (comic == null)
			{
				return NotFound();
			}

			// Get list of field definitions with options
			List<Models.CrowdSourcedFieldDefinition> fields = await _context
				.CrowdSourcedFieldDefinitions
				.Include(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.Where(csfd => csfd.IsActive && !csfd.IsDeleted)
				.OrderBy(csfd => csfd.DisplayOrder)
				.ToListAsync();

			var dctVerifiedEntries = comic.CrowdSourcedFieldVerifiedEntries.ToDictionary(csfve => csfve.CrowdSourcedFieldDefinitionId);
			var lkpUserEntries = comic.CrowdSourcedFieldUserEntries.ToLookup(csfue => csfue.CrowdSourcedFieldDefinitionId);

			// Map comic to view model
			var comicVM = modelMapper.MapComic(comic,
				mapImportedByUser: true,
				mapLastUpdatedUser: true,
				mapTranscript: true,
				mapTags: true);

			List<Shared.Models.ComicTagViewModel> uniqueTags = await _tagRepo.GetSystemTags();
			_tagRepo.SetSystemTagCounts(comicVM, uniqueTags);

			// Map field definitions and options to view model
			comicVM.ComicFields = fields.Select(csfd => new Shared.Models.ComicFieldViewModel()
			{
				Code = csfd.Code,
				Type = csfd.Type,
				DisplayOrder = csfd.DisplayOrder,
				Name = csfd.Name,
				ShortDescription = csfd.ShortDescription,
				LongDescription = csfd.LongDescription,
				CreatedDate = csfd.CreatedDate,
				LastUpdatedDate = csfd.LastUpdatedDate,
				Options = csfd.CrowdSourcedFieldDefinitionOptions
					.OrderBy(csfdo => csfdo.DisplayOrder)
					.Select(modelMapper.MapCrowdSourcedFieldDefinitionOption)
					.ToList(),
				UserEntries = lkpUserEntries.Contains(csfd.Id)
					? lkpUserEntries[csfd.Id].Select(csfue => modelMapper.MapCrowdSourcedFieldUserEntry(csfue, mapCreatedBy: true)).ToList()
					: new List<Shared.Models.CrowdSourcedFieldUserEntryViewModel>(),
				VerifiedEntry = dctVerifiedEntries.ContainsKey(csfd.Id)
					? modelMapper.MapCrowdSourcedFieldVerifiedEntry(dctVerifiedEntries[csfd.Id])
					: null,
			}).ToList();

			// Calculate comic completion percentage.
			// Each field = 0, 1, or 2 points.
			// Verified = 2 points, Collecting = 1 points, No Data = 0 points.
			// Transcript = 1 point, Tag(s) = 1 point.
			// Which is implemented by: (CountUserEntries + CountVerifiedEntries) / (TotalCountFields * 2)
			// Ignore fields where type is Section.
			var nonSectionFields = comicVM.ComicFields.Where(cf => cf.Type != CrowdSourcedFieldType.Section);
			var totalPoints = (nonSectionFields.Count() * 2) + 2;
			var accruedPoints = nonSectionFields.Sum(cf => cf.VerifiedEntry != null ? 2 : (cf.UserEntries.Any() ? 1 : 0)) + 
				(!string.IsNullOrEmpty(comicVM.Transcript.TranscriptContent) ? 1 : 0) +
				(comicVM.Tags.TagValues.Any() ? 1 : 0);
			var progress = totalPoints == 0 ? 0.0 : (totalPoints == accruedPoints ? 1.0 : ((double)accruedPoints / (double)totalPoints));

			// Wrap in comic page view model for navigation
			var firstComicCode = (await _context.Comics.OrderBy(c => c.Id).FirstAsync()).Code;
			var previousComicCode = (await _context.Comics.Where(c => c.Id == comic.Id - 1).FirstOrDefaultAsync())?.Code;
			var nextComicCode = (await _context.Comics.Where(c => c.Id == comic.Id + 1).FirstOrDefaultAsync())?.Code;
			var latestComicCode = (await _context.Comics.OrderBy(c => c.Id).LastAsync()).Code;
			var comicPageVM = new Shared.Models.ComicPageViewModel()
			{
				ComicViewModel = comicVM,
				FirstComicCode = firstComicCode,
				PreviousComicCode = previousComicCode,
				NextComicCode = nextComicCode,
				LatestComicCode = latestComicCode,
				Progress = progress,
			};

			// Cache this for later
			await _distCache.SetAsync(Services.CacheKeys.LACS.GetComic(code), comicPageVM, new DistributedCacheEntryOptions()
			{
				SlidingExpiration = TimeSpan.FromHours(1),
			});
			return Json(comicPageVM);
		}

		[HttpPut]
		[Route("{comicCode}/entry/{fieldCode}")]
		[Authorize]
		public async Task<IActionResult> PutComicUserEntry(string comicCode, string fieldCode, [FromBody] List<string> values)
		{
			UserEntrySubmissionResult result = UserEntrySubmissionResult.NewEntryAdded;

			/* Gather information */
			var userId = _userManager.GetUserId(User);
			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound();
			}
			var fieldDefinition = await _context.CrowdSourcedFieldDefinitions
				.Include(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.FirstOrDefaultAsync(csfd => csfd.Code == fieldCode);
			if (fieldDefinition == null)
			{
				return NotFound();
			}

			/* Validate and fix Values */
			if (values == null || !values.Any())
			{
				return BadRequest("Values is required");
			}
			// Order by order of original option DisplayOrder
			values = values.OrderBy(v => 
				fieldDefinition
					.CrowdSourcedFieldDefinitionOptions
					.FirstOrDefault(csfdo => csfdo.Code == v)
					?.DisplayOrder ?? -1
			).ToList();

			// Get the user's entry, if it exists.
			var userEntry = await _context.CrowdSourcedFieldUserEntries
				.Include(csfue => csfue.CrowdSourcedFieldUserEntryValues)
				.FirstOrDefaultAsync(csfue => csfue.ComicId == comic.Id &&
											  csfue.CrowdSourcedFieldDefinitionId == fieldDefinition.Id &&
											  csfue.CreatedBy == userId);

			// Perform the following changes under a transaction
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					/* Change User Entry */
					{
						// Create the user entry if it didn't already exist.
						if (userEntry == null)
						{
							result = UserEntrySubmissionResult.NewEntryAdded;
							userEntry = new Models.CrowdSourcedFieldUserEntry()
							{
								ComicId = comic.Id,
								CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
								CreatedBy = userId,
								CreatedDate = DateTimeOffset.Now,
								CrowdSourcedFieldUserEntryValues = new List<Models.CrowdSourcedFieldUserEntryValue>(),
							};
							_context.CrowdSourcedFieldUserEntries.Add(userEntry);
							await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateAddUserEntryLog(comic, userEntry, newValues: values.ToArray()));
							await _context.SaveChangesAsync();
						}
						else
						{
							result = UserEntrySubmissionResult.ExistingEntryEdited;
							userEntry.LastUpdatedDate = DateTimeOffset.Now;
							await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateEditUserEntryLog(comic, userEntry, newValues: values.ToArray()));
							await _context.SaveChangesAsync();
						}

						// First, remove all values
						var currentValues = await _context
							.CrowdSourcedFieldUserEntryValues
							.Where(csfuev => csfuev.ComicId == comic.Id && csfuev.CrowdSourcedFieldDefinitionId == fieldDefinition.Id && csfuev.CreatedBy == userId)
							.ToListAsync();
						_context.CrowdSourcedFieldUserEntryValues.RemoveRange(currentValues);
						await _context.SaveChangesAsync();

						// Next, re-add them as new rows.
						// This is easier than attempting to edit in place.
						var newUserEntryValues = values.Select((value, index) => new Models.CrowdSourcedFieldUserEntryValue()
						{
							ComicId = comic.Id,
							CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
							CreatedBy = userId,
							Id = index,
							Value = value,
						}).ToList();

						// Finally, save these to storage.
						_context.CrowdSourcedFieldUserEntryValues.AddRange(newUserEntryValues);
						await _context.SaveChangesAsync();
					}
				
					/* Perform Verification */
					{
						// Retrieve all user entries for this comic's field.
						List<Models.CrowdSourcedFieldUserEntry> userEntriesForField = await _context.CrowdSourcedFieldUserEntries
							.Include(csfue => csfue.CrowdSourcedFieldUserEntryValues)
							.Where(csfue => csfue.ComicId == comic.Id && csfue.CrowdSourcedFieldDefinitionId == fieldDefinition.Id)
							.ToListAsync();
						// Entry Values => Entry lookup
						ILookup<string[], Models.CrowdSourcedFieldUserEntry> lkpUserEntriesForField = userEntriesForField
							.ToLookup(csfue => csfue.CrowdSourcedFieldUserEntryValues.Select(csfuev => csfuev.Value).ToArray(), // The Key of the lookup is the series of values.
								new Shared.Utilities.ArrayEqualityComparer<string>()); // Use a proper comparer for arrays of strings.
						// Order the above lookup by number of entries for each overall value
						List<IGrouping<string[], Models.CrowdSourcedFieldUserEntry>> userEntriesForFieldRanked = lkpUserEntriesForField
							.OrderByDescending(lkpCsfue => lkpCsfue.Key.Length)
							.ToList();
						// Pick the top and second to top entry value sets, which will
						// be compared against below.
						IGrouping<string[], Models.CrowdSourcedFieldUserEntry> topUserEntryValues = userEntriesForFieldRanked.First();
						IGrouping<string[], Models.CrowdSourcedFieldUserEntry>? secondUserEntryValues = userEntriesForFieldRanked.Skip(1).FirstOrDefault();

						/*
						 * The criteria for one set of values to be verified is as follows.
						 * 
						 * If the difference in number of entries for one value set and the next 
						 * (or if the next does not exist, then assume this number is 0) exceeds the set threshold, 
						 * then the top number of entries becomes verified.
						 */
						int threshold = 3;
						int numEntriesTopValueSet = lkpUserEntriesForField[topUserEntryValues.Key].Count();
						int numEntriesNextValueSet = secondUserEntryValues != null ? lkpUserEntriesForField[secondUserEntryValues.Key].Count() : 0;
						bool shouldVerify = (numEntriesTopValueSet - numEntriesNextValueSet) >= threshold;

						var verifiedEntry = await _context.CrowdSourcedFieldVerifiedEntries
								.Include(csfve => csfve.CrowdSourcedFieldVerifiedEntryValues)
								.FirstOrDefaultAsync(csfve => csfve.ComicId == comic.Id &&
													 csfve.CrowdSourcedFieldDefinitionId == fieldDefinition.Id);
						if (shouldVerify)
						{
							// Create a new VerifiedEntry record, if one doesn't already exist.
							if (verifiedEntry == null)
							{
								result = result.ToVerified();

								var firstUserEntry = lkpUserEntriesForField[topUserEntryValues.Key].OrderBy(csfue => csfue.CreatedDate).First();
								verifiedEntry = new Models.CrowdSourcedFieldVerifiedEntry()
								{
									ComicId = comic.Id,
									CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
									FirstCreatedBy = firstUserEntry.CreatedBy,
									VerificationDate = DateTimeOffset.Now,
									CrowdSourcedFieldVerifiedEntryValues = new List<Models.CrowdSourcedFieldVerifiedEntryValue>(),
								};
								_context.CrowdSourcedFieldVerifiedEntries.Add(verifiedEntry);
								await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateAddVerifiedEntryLog(comic, verifiedEntry));

								// Remove old verified entry values and add new ones.
								var verifiedEntryValues = firstUserEntry.CrowdSourcedFieldUserEntryValues
									.Select(csfuev => new Models.CrowdSourcedFieldVerifiedEntryValue()
									{
										ComicId = comic.Id,
										CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
										Id = csfuev.Id,
										Value = csfuev.Value
									})
									.ToList();
								_context.CrowdSourcedFieldVerifiedEntryValues.AddRange(verifiedEntryValues);

								// Save changes
								await _context.SaveChangesAsync();
							}
						}
						else
						{
							// Threshold not met, remove any VerifiedEntry previously added.
							if (verifiedEntry != null)
							{
								_context.CrowdSourcedFieldVerifiedEntryValues.RemoveRange(verifiedEntry.CrowdSourcedFieldVerifiedEntryValues);
								_context.CrowdSourcedFieldVerifiedEntries.Remove(verifiedEntry);
								await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateRemoveVerifiedEntryLog(comic, verifiedEntry));

								await _context.SaveChangesAsync();
							}
						}
					}

					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear cache for this comic
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));

			return Json(result);
		}

		[HttpPut]
		[Route("{comicCode}/edit")]
		[Authorize(Roles = Roles.AdminMod)]
		public async Task<IActionResult> PutComicMetadata([FromRoute] string comicCode, [FromBody] Shared.Models.ComicViewModel vm)
		{
			var userId = _userManager.GetUserId(User);
			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Id == vm.Id);
			if (comic == null)
			{
				return NotFound("Comic code not found");
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					await _context.ComicHistoryLogs.AddRangeAsync(_historyLogger.CreateMetadataEditedLogs(comic, vm, userId));

					comic.Code = vm.Code;
					comic.Permalink = vm.Permalink;
					comic.Title = vm.Title;
					comic.Tooltip = vm.Tooltip;
					comic.Description = vm.Description;
					comic.ImageUrlSrc = vm.ImageUrlSrc;
					comic.ImageThumbnailUrlSrc = vm.ImageThumbnailUrlSrc;
					comic.ImageWideThumbnailUrlSrc = vm.ImageWideThumbnailUrlSrc;

					comic.LastUpdatedBy = userId;
					comic.LastUpdatedDate = DateTimeOffset.Now;

					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear cache for this comic
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.ComicIndex);
			
			return Ok();
		}

		[HttpGet]
		[Route("{comicCode}/transcript")]
		public async Task<IActionResult> GetTranscriptHistory([FromRoute] string comicCode)
		{
			var modelMapper = new Services.ModelMapper();

			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound("Comic code not found");
			}

			var transcriptHistories = await _context
				.ComicTranscriptHistories
				.Include(cth => cth.CreatedByUser)
				.Where(cth => cth.ComicId == comic.Id)
				.OrderBy(cth => cth.Id)
				.ToListAsync();

			var transcriptHistoryItemVms = transcriptHistories
				.Select(cth => modelMapper.MapTranscriptHistory(cth, mapCreatedByUser: true));

			return Json(transcriptHistoryItemVms);
		}

		[HttpPost]
		[Route("{comicCode}/transcript")]
		[Authorize]
		public async Task<IActionResult> PostTranscriptHistory(
			[FromRoute] string comicCode, 
			[FromBody] Shared.Models.TranscriptViewModel transcript)
		{
			var userId = _userManager.GetUserId(User);

			var comic = await _context
				.Comics
				.Include(c => c.ComicTranscript)
				.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound("Comic code not found");
			}

			string transcriptContent = transcript.TranscriptContent.Trim();
			if (string.IsNullOrEmpty(transcriptContent))
			{
				return BadRequest("Transcript content is required");
			}
			if (comic.ComicTranscript != null && transcriptContent.Equals(comic.ComicTranscript.TranscriptContent))
			{
				return Ok();
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					Models.ComicTranscriptHistory? latestHistory = _context
						.ComicTranscriptHistories
						.Where(cth => cth.ComicId == comic.Id)
						.OrderByDescending(cth => cth.Id)
						.FirstOrDefault();

					// Create new transcript history
					var comicTranscriptHistory = new Models.ComicTranscriptHistory()
					{
						ComicId = comic.Id,
						CreatedBy = userId,
						CreatedDate = DateTimeOffset.Now,
						TranscriptContent = transcriptContent,
					};
					_context.ComicTranscriptHistories.Add(comicTranscriptHistory);

					// Calculate diff
					if (latestHistory != null)
					{
						var diffModel = DiffPlex.DiffBuilder.SideBySideDiffBuilder.Instance.BuildDiffModel(latestHistory.TranscriptContent, transcriptContent);
						comicTranscriptHistory.DiffWithPrevious = await _renderer.RenderPartialToStringAsync("~/Pages/Diff/Diff.cshtml", diffModel);
					}
					await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateNewTranscriptLog(comic, latestHistory, comicTranscriptHistory));
					await _context.SaveChangesAsync();

					// Get and update current transcript
					var comicTranscript = await _context.ComicTranscripts
						.FirstOrDefaultAsync(ct => ct.ComicId == comic.Id);
					if (comicTranscript == null)
					{
						comicTranscript = new Models.ComicTranscript()
						{
							ComicId = comic.Id,
						};
						_context.ComicTranscripts.Add(comicTranscript);
					}
					comicTranscript.LastEditedBy = userId;
					comicTranscript.LastEditedDate = DateTimeOffset.Now;
					comicTranscript.TranscriptContent = transcriptContent;
					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "There was an error posting the transcript history item");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear cache for this comic
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));

			return Ok();
		}

		[HttpPost]
		[Route("{comicCode}/transcript/{id}")]
		[Authorize]
		public async Task<IActionResult> PostTranscriptHistoryRollback(
			[FromRoute] string comicCode, 
			[FromRoute] int id)
		{
			var userId = _userManager.GetUserId(User);

			var comic = await _context
				.Comics
				.Include(c => c.ComicTranscript)
				.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound("Comic code not found");
			}

			var sourceTranscriptHistory = await _context
				.ComicTranscriptHistories
				.FirstOrDefaultAsync(cth => cth.ComicId == comic.Id && cth.Id == id);
			if (sourceTranscriptHistory == null)
			{
				return NotFound("Comic transition history not found: " + id);
			}

			Models.ComicTranscriptHistory? latestHistory = _context
				.ComicTranscriptHistories
				.Where(cth => cth.ComicId == comic.Id)
				.OrderByDescending(cth => cth.Id)
				.FirstOrDefault();

			if (latestHistory == null)
			{
				return BadRequest("Unexpected error");
			}

			if (latestHistory.TranscriptContent == sourceTranscriptHistory.TranscriptContent)
			{
				return BadRequest("Can not roll back to a transcript that is identical");
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					// Create new transcript history
					var comicTranscriptHistory = new Models.ComicTranscriptHistory()
					{
						ComicId = comic.Id,
						CreatedBy = userId,
						CreatedDate = DateTimeOffset.Now,
						TranscriptContent = sourceTranscriptHistory.TranscriptContent,
					};
					_context.ComicTranscriptHistories.Add(comicTranscriptHistory);

					// Calculate diff
					var diffModel = DiffPlex.DiffBuilder.SideBySideDiffBuilder.Instance.BuildDiffModel(latestHistory.TranscriptContent, sourceTranscriptHistory.TranscriptContent);
					comicTranscriptHistory.DiffWithPrevious = await _renderer.RenderPartialToStringAsync("~/Pages/Diff/Diff.cshtml", diffModel);

					await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateTranscriptRollbackLog(comic, latestHistory, comicTranscriptHistory));
					await _context.SaveChangesAsync();

					// Get and update current transcript
					var comicTranscript = await _context.ComicTranscripts
						.FirstOrDefaultAsync(ct => ct.ComicId == comic.Id);
					if (comicTranscript == null)
					{
						comicTranscript = new Models.ComicTranscript()
						{
							ComicId = comic.Id,
						};
						_context.ComicTranscripts.Add(comicTranscript);
					}
					comicTranscript.LastEditedBy = userId;
					comicTranscript.LastEditedDate = DateTimeOffset.Now;
					comicTranscript.TranscriptContent = sourceTranscriptHistory.TranscriptContent;
					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "There was an error posting the transcript history item");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear cache for this comic
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));

			return Ok();
		}

		[HttpGet]
		[Route("{comicCode}/history")]
		[Authorize(Roles = Roles.AdminMod)]
		public async Task<IActionResult> GetComicHistory([FromRoute] string comicCode)
		{
			var modelMapper = new Services.ModelMapper();
			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound();
			}

			var comicHistoryLogs = await _context
				.ComicHistoryLogs
				.Include(chl => chl.Comic)
				.Include(chl => chl.CreatedByUser)
				.Include(chl => chl.CrowdSourcedFieldDefinition)
				.Where(chl => chl.ComicId == comic.Id)
				.OrderByDescending(chl => chl.Id)
				.ToListAsync();

			var vm = modelMapper.MapComicHistoryLog(comic, comicHistoryLogs);
			return Json(vm);
		}

		[HttpPut]
		[Route("{comicCode}/tags")]
		[Authorize]
		public async Task<IActionResult> PutComicTags(
			[FromRoute] string comicCode,
			[FromBody] Shared.Models.ComicTagsViewModel vm)
		{
			var userId = _userManager.GetUserId(User);
			
			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound();
			}

			// Enforce casing requirements
			foreach (var tagVm in vm.TagValues)
			{
				tagVm.TagValue = Shared.Models.ComicTagViewModel.Transform(tagVm.TagValue);
			}

			// Get current tags
			var comicTags = await _context.ComicTags
				.Where(ct => ct.ComicId == comic.Id)
				.ToListAsync();

			// Calculate new and deleted tags
			var dctCurrentValues = comicTags.ToDictionary(ct => ct.Value);
			var hshNewValues = new HashSet<string>(vm.TagValues.Select(tv => tv.TagValue));
			var addedValues = hshNewValues.Except(dctCurrentValues.Keys);
			var deletedValues = dctCurrentValues.Keys.Except(hshNewValues);

			if (!addedValues.Any() && !deletedValues.Any())
			{
				return Ok();
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					// Remove deleted tags
					foreach (var deletedValue in deletedValues)
					{
						var comicTag = dctCurrentValues[deletedValue];
						_context.ComicTags.Remove(comicTag);
					}
					await _context.SaveChangesAsync();

					// Add new tags
					foreach (var newValue in addedValues)
					{
						var comicTag = new Models.ComicTag()
						{
							ComicId = comic.Id,
							Value = newValue,
							CreatedBy = userId,
							CreatedDate = DateTimeOffset.Now,
						};
						_context.ComicTags.Add(comicTag);
					}
					await _context.SaveChangesAsync();

					// History
					await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreatePutComicTagsLog(comic, userId, addedValues, deletedValues));
					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "There was an error putting the comic tags");
					await transaction.RollbackAsync();
					throw;
				}
			}

			// Clear comic from cache
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.GetComic(comicCode));
			await _distCache.RemoveAsync(Services.CacheKeys.LACS.Tags.Index);

			return Ok();
		}

		[HttpGet]
		[Route("integrity_queue")]
		public async Task<IEnumerable<Shared.Models.ComicListItemViewModel>> GetLowIntegrityComics()
		{
			var modelMapper = new Services.ModelMapper();
			var comicsQuery = _context.Comics
				.Include(c => c.CrowdSourcedFieldUserEntries)
				.Include(c => c.CrowdSourcedFieldVerifiedEntries)
				.Include(c => c.ComicTranscript)
				.Include(c => c.ComicTags)
				.OrderBy(c => 
					c.CrowdSourcedFieldUserEntries.GroupBy(csfue => csfue.CrowdSourcedFieldDefinitionId).Count() + 
					c.CrowdSourcedFieldVerifiedEntries.Count() + 
					(c.ComicTranscript != null ? 1 : 0) +
					(c.ComicTags.Any() ? 1 : 0))
				.Take(10);
			// _logger.LogInformation(comicsQuery.ToQueryString());
			var comics = await comicsQuery.ToListAsync();

			var fieldDefinitionCount = _context.CrowdSourcedFieldDefinitions
				.Where(csfd => csfd.IsActive && !csfd.IsDeleted && csfd.Type != CrowdSourcedFieldType.Section)
				.Count();
			var totalPoints = (fieldDefinitionCount * 2) + 2;

			return comics
				.Select(c => {
					var cli = modelMapper.MapComicListItem(c);
					cli.Integrity = (int)Math.Floor((double)(
						c.CrowdSourcedFieldUserEntries.GroupBy(csfue => csfue.CrowdSourcedFieldDefinitionId).Count() + 
						c.CrowdSourcedFieldVerifiedEntries.Count() + 
						(!string.IsNullOrEmpty(c.ComicTranscript?.TranscriptContent) ? 1 : 0) +
						(c.ComicTags.Any() ? 1 : 0)
					) / (double)totalPoints * 100.0);
					return cli;
				})
				.Where(c => c.Integrity < totalPoints);
		}
	}
}
