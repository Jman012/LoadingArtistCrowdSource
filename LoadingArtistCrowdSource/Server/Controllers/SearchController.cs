using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared.Enums;
using LoadingArtistCrowdSource.Shared.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[ApiController]
	[Route("api/search")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class SearchController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SearchController(ApplicationDbContext context)
		{
			this._context = context;
		}

		[HttpGet]
		[Route("fields")]
		public async Task<IEnumerable<ComicFieldViewModel>> Fields()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			List<Models.CrowdSourcedFieldDefinition> fields = await _context
				.CrowdSourcedFieldDefinitions
				.Include(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.Where(csfd => csfd.IsActive && !csfd.IsDeleted)
				.ToListAsync();

			return fields.Select(csfd => new ComicFieldViewModel()
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
			}).ToList();
		}

		[HttpPost]
		public async Task<IEnumerable<ComicViewModel>> Search([FromBody] SearchViewModel vm)
		{

			return new ComicViewModel[] { };
		}
	}
}
