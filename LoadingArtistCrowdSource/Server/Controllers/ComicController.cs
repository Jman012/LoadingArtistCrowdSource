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

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[ApiController]
	[Route("api/comic")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class ComicController : Controller
	{
		private readonly ApplicationDbContext _context;
		public ComicController(ApplicationDbContext context)
		{
			this._context = context;
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

			List<Models.CrowdSourcedFieldDefinition> fields = await _context.CrowdSourcedFieldDefinitions
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
				UserEntries = lkpUserEntries.Contains(csfd.Id)
					? lkpUserEntries[csfd.Id].Select(csfue => modelMapper.MapCrowdSourcedFieldUserEntry(csfue, mapCreatedBy: true)).ToList()
					: new List<Shared.Models.CrowdSourcedFieldUserEntryViewModel>(),
				VerifiedEntry = dctVerifiedEntries.ContainsKey(csfd.Id)
					? modelMapper.MapCrowdSourcedFieldVerifiedEntry(dctVerifiedEntries[csfd.Id])
					: null,
			}).ToList();

			return Json(comicVM);
		}
	}
}
