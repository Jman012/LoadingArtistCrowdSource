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
using Microsoft.AspNetCore.Identity;

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
		private readonly UserManager<Models.ApplicationUser> _userManager;
		public FieldController(ApplicationDbContext context, UserManager<Models.ApplicationUser> userManager)
		{
			this._context = context;
			this._userManager = userManager;
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
		[Route("{code}")]
		public async Task<IActionResult> GetField(string code)
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			Models.CrowdSourcedFieldDefinition? fieldDef = await _context.CrowdSourcedFieldDefinitions
				.Include(csdf => csdf.CreatedByUser)
				.Include(csdf => csdf.LastUpdatedByUser)
				.Include(csdf => csdf.CrowdSourcedFieldDefinitionOptions)
				.FirstOrDefaultAsync(c => c.Code == code);

			if (fieldDef == null || fieldDef.IsDeleted)
			{
				return NotFound();
			}

			return Json(modelMapper.MapFieldDefinitionForm(fieldDef, mapOptions: true));
		}

		[HttpPut]
		[Route("{code}")]
		public async Task<IActionResult> PutField(Shared.Models.FieldDefinitionFormViewModel vm)
		{
			var userId = _userManager.GetUserId(User);
			var fieldDef = await _context.CrowdSourcedFieldDefinitions
				.Include(csfd => csfd.CreatedByUser)
				.Include(csfd => csfd.LastUpdatedByUser)
				.Include(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.FirstOrDefaultAsync(csfd => csfd.Code == vm.Code);

			if (fieldDef == null)
			{
				fieldDef = new Models.CrowdSourcedFieldDefinition()
				{
					Code = vm.Code,
					CreatedDate = DateTimeOffset.Now,
					CreatedBy = userId,
				};
				_context.CrowdSourcedFieldDefinitions.Add(fieldDef);
			}

			fieldDef.IsActive = vm.IsActive;
			fieldDef.Type = vm.Type;
			fieldDef.DisplayOrder = 999;
			fieldDef.Name = vm.Name;
			fieldDef.ShortDescription = vm.ShortDescription;
			fieldDef.LongDescription = vm.LongDescription;
			fieldDef.LastUpdatedDate = DateTimeOffset.Now;
			fieldDef.LastUpdatedBy = userId;

			var hshFieldDefOptionCodes = new HashSet<string>(fieldDef.CrowdSourcedFieldDefinitionOptions.Select(csfdo => csfdo.Code));
			var hshOptionVMCodes = new HashSet<string>(vm.Options.Select(o => o.Code));
			// Delete
			fieldDef.CrowdSourcedFieldDefinitionOptions.RemoveAll(csfdo => !hshOptionVMCodes.Contains(csfdo.Code));
			// Update
			foreach (var pair in fieldDef.CrowdSourcedFieldDefinitionOptions.Join(vm.Options, csfdo => csfdo.Code, ovm => ovm.Code, (csfdo, ovm) => Tuple.Create(csfdo, ovm)))
			{
				var csfdo = pair.Item1;
				var ovm = pair.Item2;

				csfdo.Text = ovm.Text;
				csfdo.Description = ovm.Description;
				csfdo.URL = ovm.URL;
			}
			// Add
			foreach (var optionVM in vm.Options.Where(ovm => !hshFieldDefOptionCodes.Contains(ovm.Code)))
			{
				fieldDef.CrowdSourcedFieldDefinitionOptions.Add(new Models.CrowdSourcedFieldDefinitionOption()
				{
					Code = optionVM.Code,
					Text = optionVM.Text,
					Description = optionVM.Description,
					URL = optionVM.URL,
				});
			}

			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
