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
	[Route("api/comics")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class ComicListController : Controller
	{
		private readonly ApplicationDbContext _context;
		public ComicListController(ApplicationDbContext context)
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
		[Route("/api/comics/{code}")]
		public async Task<IActionResult> GetComic(string code)
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			Models.Comic? comic = await _context.Comics
				.Include(c => c.ImportedByUser)
				.Include(c => c.LastUpdatedByUser)
				.Include(c => c.CrowdSourcedFieldVerifiedEntries)
				.ThenInclude(ve => ve.CrowdSourcedFieldVerifiedEntryValues)
				.Include(c => c.CrowdSourcedFieldVerifiedEntries)
				.ThenInclude(ve => ve.CrowdSourcedFieldDefinition)
				.FirstOrDefaultAsync(c => c.Code == code);

			if (comic == null)
			{
				return NotFound();
			}

			return Json(modelMapper.MapComic(comic,
				mapImportedByUser: true,
				mapLastUpdatedUser: true,
				mapVerifiedEntries: true));
		}
	}
}
