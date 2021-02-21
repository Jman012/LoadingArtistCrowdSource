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
			IQueryable<Models.Comic> query = _context.Comics
				.Include(c => c.ImportedByUser)
				.Include(c => c.LastUpdatedByUser);

			if (vm.Id.HasValue)
			{
				query = query.Where(c => c.Id == vm.Id.Value);
			}
			if (!string.IsNullOrEmpty(vm.Code))
			{
				query = query.Where(c => c.Code.Contains(vm.Code));
			}
			if (!string.IsNullOrEmpty(vm.Title))
			{
				query = query.Where(c => c.Title.Contains(vm.Title));
			}
			if (!string.IsNullOrEmpty(vm.Description))
			{
				query = query.Where(c => c.Description != null && c.Description.Contains(vm.Description));
			}
			foreach (var searchEntry in vm.SearchEntries)
			{
				string[] values = searchEntry.FieldValues.Where(v => v.Filtered).Select(v => v.Code).ToArray();
				if (!values.Any())
				{
					continue;
				}

				switch (searchEntry.Operator)
				{
					case SearchEntryOperator.Any:
						query = query.Where(c =>
							c.CrowdSourcedFieldVerifiedEntries.Any(csfve =>
								csfve.CrowdSourcedFieldDefinition.Code == searchEntry.FieldCode
								&& csfve.CrowdSourcedFieldVerifiedEntryValues.Any(csfvev => values.Contains(csfvev.Value))));
						break;
					case SearchEntryOperator.All:
						query = query.Where(c =>
							c.CrowdSourcedFieldVerifiedEntries.Any(csfve =>
								csfve.CrowdSourcedFieldDefinition.Code == searchEntry.FieldCode
								&& csfve.CrowdSourcedFieldVerifiedEntryValues.All(csfvev => values.Contains(csfvev.Value))));
						break;
					default:
						throw new Exception($"Unhandled {nameof(SearchEntryOperator)} value '{searchEntry.Operator}'");
				}
			}

			var comics = await query.OrderBy(c => c.Id).ToListAsync();

			Services.ModelMapper modelMapper = new Services.ModelMapper();
			return comics.Select(c => modelMapper.MapComic(c));
		}
	}
}
