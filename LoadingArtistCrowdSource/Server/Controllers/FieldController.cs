using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[ApiController]
	[Route("api/field")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class FieldController : Controller
	{
		private readonly ApplicationDbContext _context;
		public FieldController(ApplicationDbContext context)
		{
			this._context = context;
		}

		[HttpGet]
		public IEnumerable<Shared.Models.CrowdSourcedFieldDefinitionViewModel> Index()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			return _context.CrowdSourcedFieldDefinitions
				.Include(f => f.CreatedByUser)
				.Include(f => f.LastUpdatedByUser)
				.OrderBy(f => f.Id)
				.ToList()
				.Select(f => modelMapper.MapCrowdSourcedFieldDefinition(f, mapCreatedByUser: true, mapLastUpdatedByUser: true));
		}

		//[HttpGet]
		//[Route("/api/field/{code}")]
		//public async Task<IActionResult> GetComic(string code)
		//{
		//	Services.ModelMapper modelMapper = new Services.ModelMapper();
		//	Models.Comic? comic = await _context.Comics
		//		.Include(c => c.ImportedByUser)
		//		.Include(c => c.LastUpdatedByUser)
		//		.Include(c => c.CrowdSourcedFieldVerifiedEntries).ThenInclude(ve => ve.CrowdSourcedFieldVerifiedEntryValues)
		//		.Include(c => c.CrowdSourcedFieldVerifiedEntries).ThenInclude(ve => ve.CrowdSourcedFieldDefinition)
		//		.Include(c => c.CrowdSourcedFieldUserEntries).ThenInclude(ue => ue.CrowdSourcedFieldUserEntryValues)
		//		.Include(c => c.CrowdSourcedFieldUserEntries).ThenInclude(ue => ue.CreatedByUser)
		//		.FirstOrDefaultAsync(c => c.Code == code);

		//	if (comic == null)
		//	{
		//		return NotFound();
		//	}

		//	return Json(modelMapper.MapComic(comic,
		//		mapImportedByUser: true,
		//		mapLastUpdatedUser: true,
		//		mapVerifiedEntries: true,
		//		mapVerifiedUserEntries: true));
		//}
	}
}
