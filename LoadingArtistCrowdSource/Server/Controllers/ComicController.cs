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

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[ApiController]
	[Route("api/comic")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class ComicController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Models.ApplicationUser> _userManager;

		public ComicController(ApplicationDbContext context, UserManager<Models.ApplicationUser> userManager)
		{
			this._context = context;
			this._userManager = userManager;
		}

		[HttpGet]
		public async Task<IEnumerable<Shared.Models.ComicViewModel>> Index()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			var comics = await _context.Comics
				.Include(c => c.ImportedByUser)
				.Include(c => c.LastUpdatedByUser)
				.OrderBy(c => c.Id)
				.ToListAsync();

			return comics.Select(c => modelMapper.MapComic(c));
		}

		[HttpGet]
		[Route("{code}")]
		public async Task<IActionResult> GetComic(string code)
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			Models.Comic? comic = await _context.Comics
				.Include(c => c.ImportedByUser)
				.Include(c => c.LastUpdatedByUser)
				.Include(c => c.CrowdSourcedFieldVerifiedEntries).ThenInclude(ve => ve.CrowdSourcedFieldVerifiedEntryValues)
				.Include(c => c.CrowdSourcedFieldUserEntries).ThenInclude(ue => ue.CrowdSourcedFieldUserEntryValues)
				.Include(c => c.CrowdSourcedFieldUserEntries).ThenInclude(ue => ue.CreatedByUser)
				.FirstOrDefaultAsync(c => c.Code == code);

			if (comic == null)
			{
				return NotFound();
			}

			List<Models.CrowdSourcedFieldDefinition> fields = await _context
				.CrowdSourcedFieldDefinitions
				.Include(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.Where(csfd => csfd.IsActive && !csfd.IsDeleted)
				.ToListAsync();

			var dctVerifiedEntries = comic.CrowdSourcedFieldVerifiedEntries.ToDictionary(csfve => csfve.CrowdSourcedFieldDefinitionId);
			var lkpUserEntries = comic.CrowdSourcedFieldUserEntries.ToLookup(csfue => csfue.CrowdSourcedFieldDefinitionId);

			var comicVM = modelMapper.MapComic(comic,
				mapImportedByUser: true,
				mapLastUpdatedUser: true);

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
				Options = csfd.CrowdSourcedFieldDefinitionOptions.Select(modelMapper.MapCrowdSourcedFieldDefinitionOption).ToList(),
				UserEntries = lkpUserEntries.Contains(csfd.Id)
					? lkpUserEntries[csfd.Id].Select(csfue => modelMapper.MapCrowdSourcedFieldUserEntry(csfue, mapCreatedBy: true)).ToList()
					: new List<Shared.Models.CrowdSourcedFieldUserEntryViewModel>(),
				VerifiedEntry = dctVerifiedEntries.ContainsKey(csfd.Id)
					? modelMapper.MapCrowdSourcedFieldVerifiedEntry(dctVerifiedEntries[csfd.Id])
					: null,
			}).ToList();

			return Json(comicVM);
		}

		[HttpPut]
		[Route("{comicCode}/entry/{fieldCode}")]
		[Authorize]
		public async Task<IActionResult> PutComicUserEntry(string comicCode, string fieldCode, [FromBody] List<string> values)
		{
			UserEntrySubmissionResult result = UserEntrySubmissionResult.NewEntryAdded;
			var userId = _userManager.GetUserId(User);

			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound();
			}

			var fieldDefinition = await _context.CrowdSourcedFieldDefinitions.FirstOrDefaultAsync(csfd => csfd.Code == fieldCode);
			if (fieldDefinition == null)
			{
				return NotFound();
			}

			var existingVerifiedEntry = await _context.CrowdSourcedFieldVerifiedEntries.FirstOrDefaultAsync(csfve => csfve.ComicId == comic.Id && csfve.CrowdSourcedFieldDefinitionId == fieldDefinition.Id);
			if (existingVerifiedEntry != null)
			{
				return BadRequest("This field is already verified");
			}

			var userEntry = await _context.CrowdSourcedFieldUserEntries.FirstOrDefaultAsync(csfue => csfue.ComicId == comic.Id && csfue.CrowdSourcedFieldDefinitionId == fieldDefinition.Id && csfue.CreatedBy == userId);

			var transaction = await _context.Database.BeginTransactionAsync();

			if (userEntry == null)
			{
				result = UserEntrySubmissionResult.NewEntryAdded;
				userEntry = new Models.CrowdSourcedFieldUserEntry()
				{
					ComicId = comic.Id,
					CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
					CreatedBy = userId,
					CreatedDate = DateTimeOffset.Now,
				};
				_context.CrowdSourcedFieldUserEntries.Add(userEntry);
				await _context.SaveChangesAsync();
			}
			else
			{
				result = UserEntrySubmissionResult.ExistingEntryEdited;
				userEntry.LastUpdatedDate = DateTimeOffset.Now;
			}

			// Remove all values
			var currentValues = await _context
				.CrowdSourcedFieldUserEntryValues
				.Where(csfuev => csfuev.ComicId == csfuev.ComicId && csfuev.CrowdSourcedFieldDefinitionId == fieldDefinition.Id && csfuev.CreatedBy == userId)
				.ToListAsync();
			_context.CrowdSourcedFieldUserEntryValues.RemoveRange(currentValues);
			await _context.SaveChangesAsync();

			// And re-add them as new rows
			var newUserEntryValues = values.Select((value, index) => new Models.CrowdSourcedFieldUserEntryValue()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
				CreatedBy = userId,
				Id = index,
				Value = value,
			}).ToList();

			_context.CrowdSourcedFieldUserEntryValues.AddRange(newUserEntryValues);
			await _context.SaveChangesAsync();

			// Attempt verification
			var userEntriesForField = await _context.CrowdSourcedFieldUserEntries
				.Include(csfue => csfue.CrowdSourcedFieldUserEntryValues)
				.Where(csfue => csfue.ComicId == comic.Id && csfue.CrowdSourcedFieldDefinitionId == fieldDefinition.Id)
				.ToListAsync();
			// Entry Values => Entry lookup
			var lkpUserEntriesForField = userEntriesForField
				.ToLookup(csfue => csfue.CrowdSourcedFieldUserEntryValues.Select(csfuev => csfuev.Value).ToArray(), new Shared.Utilities.ArrayEqualityComparer<string>());
			// Order the above lookup by number of entries for each overall value
			var userEntriesForFieldRanked = lkpUserEntriesForField
				.OrderBy(lkpCsfue => lkpCsfue.Key.Length)
				.ToList();
			var topUserEntryValues = userEntriesForFieldRanked.First();
			var secondUserEntryValues = userEntriesForFieldRanked.Skip(1).FirstOrDefault();
			// Once the threshold has been met (at least three user entries with the same value)
			bool shouldVerify = false;
			if (lkpUserEntriesForField[topUserEntryValues.Key].Count() >= 3)
			{
				if (secondUserEntryValues != null)
				{
					// If other values do exist, then we don't want the verified answer to only edge by.
					// It should exceed the next most answered by a fair margin.
					if (topUserEntryValues.Key.Length - secondUserEntryValues.Key.Length > 3)
					{
						shouldVerify = true;
					}
				}
				else
				{
					shouldVerify = true;
				}
			}

			if (shouldVerify)
			{
				result = result.ToVerified();
				var firstUserEntry = lkpUserEntriesForField[topUserEntryValues.Key].OrderBy(csfue => csfue.CreatedDate).First();
				var verifiedEntry = new Models.CrowdSourcedFieldVerifiedEntry()
				{
					ComicId = comic.Id,
					CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
					FirstCreatedBy = firstUserEntry.CreatedBy,
					VerificationDate = DateTimeOffset.Now,
				};
				var verifiedEntryValues = firstUserEntry.CrowdSourcedFieldUserEntryValues
					.Select(csfuev => new Models.CrowdSourcedFieldVerifiedEntryValue()
					{
						ComicId = comic.Id,
						CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
						Id = csfuev.Id,
						Value = csfuev.Value
					})
					.ToList();
				_context.CrowdSourcedFieldVerifiedEntries.Add(verifiedEntry);
				_context.CrowdSourcedFieldVerifiedEntryValues.AddRange(verifiedEntryValues);
				await _context.SaveChangesAsync();
			}

			await transaction.CommitAsync();

			return Json(result);
		}
	}
}
