using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared.Enums;
using LoadingArtistCrowdSource.Shared.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
			_context = context;
		}

		[HttpGet]
		[Route("fields")]
		public async Task<IEnumerable<ComicFieldViewModel>> Fields()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			List<Models.CrowdSourcedFieldDefinition> fields = await _context
				.CrowdSourcedFieldDefinitions
				.Include(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.Include(csfd => csfd.CrowdSourcedFieldVerifiedEntryValues)
				.Where(csfd => csfd.IsActive && !csfd.IsDeleted)
				.OrderBy(csfd => csfd.DisplayOrder)
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
				Options = csfd.CrowdSourcedFieldDefinitionOptions
					.OrderBy(csfdo => csfdo.DisplayOrder)
					.Select(modelMapper.MapCrowdSourcedFieldDefinitionOption).ToList(),
				UniqueVerifiedValues = csfd.CrowdSourcedFieldVerifiedEntryValues.Select(csfvev => csfvev.Value).Distinct().ToList(),
			}).ToList();
		}

		[HttpPost]
		public async Task<IEnumerable<ComicListItemViewModel>> Search([FromBody] SearchViewModel vm)
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
			if (!string.IsNullOrEmpty(vm.Tooltip))
			{
				query = query.Where(c => c.Tooltip != null && c.Tooltip.Contains(vm.Tooltip));
			}
			if (!string.IsNullOrEmpty(vm.Description))
			{
				query = query.Where(c => c.Description != null && c.Description.Contains(vm.Description));
			}
			if (!string.IsNullOrEmpty(vm.Transcript))
			{
				query = query.Where(c => c.ComicTranscript != null && c.ComicTranscript.TranscriptContent.Contains(vm.Transcript));
			}
			if (vm.Tags.Any())
			{
				ISet<string> values = new HashSet<string>(vm.Tags.Select(t => t.TagValue));
				switch (vm.TagsOperator)
				{
					case SearchEntryOperator.Any:
						query = query.Where(c =>
							c.ComicTags.Any(ct => 
								values.Contains(ct.Value)));
						break;
					case SearchEntryOperator.All:
						foreach (string value in vm.Tags.Select(t => t.TagValue))
						{
							query = query.Where(c => c.ComicTags.Any(ct => ct.Value == value));
						}
						break;
					default:
						throw new Exception($"Unhandled {nameof(SearchEntryOperator)} value '{vm.TagsOperator}'");
				}
			}
			foreach (var searchEntry in vm.SearchEntries)
			{
				string[] values = searchEntry.FieldValues.Where(v => !string.IsNullOrEmpty(v.Code) && v.Filtered).Select(v => v.Code).ToArray();
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
						foreach (string value in values)
						{
							query = query.Where(c => c.CrowdSourcedFieldVerifiedEntries.Any(csfve =>
								csfve.CrowdSourcedFieldDefinition.Code == searchEntry.FieldCode
								&& csfve.CrowdSourcedFieldVerifiedEntryValues.Any(csfvev => csfvev.Value == value)));
						}
						break;
					default:
						throw new Exception($"Unhandled {nameof(SearchEntryOperator)} value '{searchEntry.Operator}'");
				}
			}

			var comics = await query.OrderBy(c => c.Id).ToListAsync();

			Services.ModelMapper modelMapper = new Services.ModelMapper();
			return comics.Select(c => modelMapper.MapComicListItem(c));
		}
	}
}
