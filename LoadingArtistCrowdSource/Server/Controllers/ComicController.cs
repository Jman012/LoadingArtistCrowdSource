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
		public IEnumerable<Shared.Models.ComicViewModel> Index()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			return _context.Comics
				.Include(c => c.ImportedByUser)
				.Include(c => c.LastUpdatedByUser)
				.OrderBy(c => c.Id)
				.ToList()
				.Select(c => modelMapper.MapComic(c));
		}

		[HttpGet]
		[Route("/api/comic/{code}")]
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
		[Route("/api/comic/{comicCode}/entry/{fieldCode}")]
		[Authorize]
		public async Task<IActionResult> PutComicUserEntry(string comicCode, string fieldCode, [FromBody] List<string> values)
		{
			var userId = _userManager.GetUserId(User);

			var comic = _context.Comics.FirstOrDefault(c => c.Code == comicCode);
			if (comic == null)
			{
				return NotFound();
			}

			var fieldDefinition = _context.CrowdSourcedFieldDefinitions.FirstOrDefault(csfd => csfd.Code == fieldCode);
			if (fieldDefinition == null)
			{
				return NotFound();
			}

			var userEntry = _context.CrowdSourcedFieldUserEntries.FirstOrDefault(csfue => csfue.ComicId == comic.Id && csfue.CrowdSourcedFieldDefinitionId == fieldDefinition.Id && csfue.CreatedBy == userId);

			var transaction = await _context.Database.BeginTransactionAsync();

			if (userEntry == null)
			{
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

			await transaction.CommitAsync();

			return Ok();
		}
	}
}
