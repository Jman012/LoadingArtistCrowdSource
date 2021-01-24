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

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[Authorize(Roles = Roles.Administrator)]
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

		[HttpGet]
		[Route("/api/field/{code}")]
		public async Task<IActionResult> GetComic(string code)
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			Models.CrowdSourcedFieldDefinition? fieldDef = await _context.CrowdSourcedFieldDefinitions
				.Include(c => c.CreatedByUser)
				.Include(c => c.LastUpdatedByUser)
				.FirstOrDefaultAsync(c => c.Code == code);

			if (fieldDef == null)
			{
				return NotFound();
			}

			return Json(modelMapper.MapFieldDefinitionForm(fieldDef));
		}
	}
}
