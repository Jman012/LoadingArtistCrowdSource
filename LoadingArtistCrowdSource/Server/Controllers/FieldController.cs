using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared;
using LoadingArtistCrowdSource.Shared.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;


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
		private readonly ILogger<FieldController> _logger;
		private readonly Services.HistoryLogger _historyLogger;
		public FieldController(
			ApplicationDbContext context, 
			UserManager<Models.ApplicationUser> userManager, 
			ILogger<FieldController> logger,
			Services.HistoryLogger historyLogger)
		{
			_context = context;
			_userManager = userManager;
			_logger = logger;
			_historyLogger = historyLogger;
		}

		[HttpGet]
		public IEnumerable<Shared.Models.CrowdSourcedFieldDefinitionViewModel> Index()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			return _context.CrowdSourcedFieldDefinitions
				.Include(f => f.CreatedByUser)
				.Include(f => f.LastUpdatedByUser)
				.OrderBy(f => f.DisplayOrder)
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

			fieldDef.CrowdSourcedFieldDefinitionOptions = fieldDef.CrowdSourcedFieldDefinitionOptions
				.OrderBy(csfdo => csfdo.DisplayOrder).ToList();

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

			if (vm.IsNewField && fieldDef != null)
			{
				return BadRequest("This Code is already taken by another field in the system.");
			}

			if (vm.Options.Count != vm.Options.Select(o => o.Code).Distinct().Count())
			{
				return BadRequest("There are duplicate codes for the field options");
			}

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					// Create definition if needed
					if (fieldDef == null)
					{
						fieldDef = new Models.CrowdSourcedFieldDefinition()
						{
							Code = vm.Code,
							CreatedDate = DateTimeOffset.Now,
							CreatedBy = userId,
						};
						_context.CrowdSourcedFieldDefinitions.Add(fieldDef);
						_context.CrowdSourcedFieldDefinitionHistoryLogs.Add(_historyLogger.CreateAddFieldDefinitionLog(fieldDef));
					}
					else
					{
						_context.CrowdSourcedFieldDefinitionHistoryLogs.AddRange(_historyLogger.CreateEditFieldDefinitionLogs(fieldDef, vm, userId));
					}

					// Set definition properties
					fieldDef.IsActive = vm.IsActive;
					fieldDef.Type = vm.Type;
					fieldDef.Name = vm.Name;
					fieldDef.ShortDescription = vm.ShortDescription;
					fieldDef.LongDescription = vm.LongDescription;
					fieldDef.LastUpdatedDate = DateTimeOffset.Now;
					fieldDef.LastUpdatedBy = userId;
					await _context.SaveChangesAsync();

					// Remove all options
					fieldDef.CrowdSourcedFieldDefinitionOptions.RemoveAll((_) => true);
					await _context.SaveChangesAsync();

					// Re-add all options with correct display order
					fieldDef.CrowdSourcedFieldDefinitionOptions.AddRange(vm.Options.Select((optionVM, index) => new Models.CrowdSourcedFieldDefinitionOption()
					{
						Code = optionVM.Code,
						Text = optionVM.Text,
						Description = optionVM.Description,
						URL = optionVM.URL,
						DisplayOrder = index,
					}));
					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Unexpected error saving field definition and options");
					await transaction.RollbackAsync();
					throw;
				}
			}

			return Ok();
		}

		[HttpPut]
		[Route("/api/field_positions")]
		public async Task<IActionResult> PutFieldPositions([FromBody] List<string> fieldCodes)
		{
			var setFieldCodes = new HashSet<string>(fieldCodes);

			var fields = await _context.CrowdSourcedFieldDefinitions
				.Where(csfd => csfd.IsActive && !csfd.IsDeleted)
				.ToListAsync();
			var setDbFieldCodes = new HashSet<string>(fields.Select(csfd => csfd.Code));

			if (!setFieldCodes.SetEquals(setDbFieldCodes))
			{
				return BadRequest("Some fields were added or removed since the page was loaded. Please refresh and try again.");
			}

			fields = fields.OrderBy(csfd => fieldCodes.IndexOf(csfd.Code)).ToList();
			fields = fields.Select((csfd, index) => {
				csfd.DisplayOrder = index;
				return csfd;
			}).ToList();
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
