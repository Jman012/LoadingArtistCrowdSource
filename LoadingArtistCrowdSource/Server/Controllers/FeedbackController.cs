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
	[Route("api/feedback")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	[Authorize]
	public class FeedbackController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Models.ApplicationUser> _userManager;

		public FeedbackController(ApplicationDbContext context, UserManager<Models.ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("")]
		public async Task<IEnumerable<FeedbackViewModel>> GetList()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			var userId = _userManager.GetUserId(User);

			List<Models.CrowdSourcedFieldDefinitionFeedback> feedbacks = await _context
				.CrowdSourcedFieldDefinitionFeedbacks
				.Include(csfdf => csfdf.Comic)
				.Include(csfdf => csfdf.CrowdSourcedFieldDefinition)
				.Include(csfdf => csfdf.CreatedByUser)
				.Include(csfdf => csfdf.CompletedByUser)
				.Where(csfdf => csfdf.CreatedBy == userId)
				.OrderBy(csfdf => csfdf.CreatedDate)
				.ToListAsync();

			return feedbacks.Select(csfdf => modelMapper.MapFeedback(csfdf, mapCreatedBy: true, mapCompletedBy: true));
		}

		[HttpPost]
		[Route("{comicCode}/{fieldCode}")]
		public async Task<IActionResult> PostFeedback(
			[FromRoute] string comicCode, 
			[FromRoute] string fieldCode, 
			[FromBody] FeedbackViewModel vm
		) {
			var userId = _userManager.GetUserId(User);

			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return BadRequest("Comic not found");
			}

			var fieldDef = await _context.CrowdSourcedFieldDefinitions.FirstOrDefaultAsync(csfd => csfd.Code == fieldCode);
			if (fieldDef == null)
			{
				return BadRequest("Field not found");
			}

			var feedback = new Models.CrowdSourcedFieldDefinitionFeedback()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = fieldDef.Id,
				CreatedBy = userId,
				CreatedDate = DateTimeOffset.Now,
				Comment = vm.Comment,
			};
			_context.CrowdSourcedFieldDefinitionFeedbacks.Add(feedback);

			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		[Route("queue")]
		[Authorize(Roles = Roles.AdminMod)]
		public async Task<IEnumerable<FeedbackViewModel>> GetQueueList()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();

			List<Models.CrowdSourcedFieldDefinitionFeedback> feedbacks = await _context
				.CrowdSourcedFieldDefinitionFeedbacks
				.Include(csfdf => csfdf.Comic)
				.Include(csfdf => csfdf.CrowdSourcedFieldDefinition)
				.Include(csfdf => csfdf.CreatedByUser)
				.Include(csfdf => csfdf.CompletedByUser)
				.OrderBy(csfdf => csfdf.CreatedDate)
				.ToListAsync();

			return feedbacks.Select(csfdf => modelMapper.MapFeedback(csfdf, mapCreatedBy: true, mapCompletedBy: true));
		}

		[HttpGet]
		[Route("{comicCode}/{fieldCode}/{id}")]
		[Authorize(Roles = Roles.AdminMod)]
		public async Task<IActionResult> GetFeedback(
			[FromRoute] string comicCode, 
			[FromRoute] string fieldCode, 
			[FromRoute] int id)
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();

			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return BadRequest("Comic not found");
			}

			var fieldDef = await _context.CrowdSourcedFieldDefinitions.FirstOrDefaultAsync(csfd => csfd.Code == fieldCode);
			if (fieldDef == null)
			{
				return BadRequest("Field not found");
			}

			Models.CrowdSourcedFieldDefinitionFeedback feedback = await _context
				.CrowdSourcedFieldDefinitionFeedbacks
				.Include(csfdf => csfdf.Comic)
				.Include(csfdf => csfdf.CrowdSourcedFieldDefinition)
				.Include(csfdf => csfdf.CreatedByUser)
				.Include(csfdf => csfdf.CompletedByUser)
				.Where(csfdf => csfdf.ComicId == comic.Id && csfdf.CrowdSourcedFieldDefinitionId == fieldDef.Id && csfdf.Id == id)
				.FirstOrDefaultAsync();

			return Json(modelMapper.MapFeedback(feedback, mapCreatedBy: true, mapCompletedBy: true));
		}

		[HttpPost]
		[Route("{comicCode}/{fieldCode}/{id}")]
		[Authorize(Roles = Roles.AdminMod)]
		public async Task<IActionResult> PostCompleteFeedback(
			[FromRoute] string comicCode, 
			[FromRoute] string fieldCode, 
			[FromRoute] int id,
			[FromBody] FeedbackViewModel vm)
		{
			var userId = _userManager.GetUserId(User);
			if (string.IsNullOrEmpty(vm.CompletionComment))
			{
				return BadRequest("Completion Comment is required.");
			}
			if (vm.CompletionType == null)
			{
				return BadRequest("Completion Type is required");
			}

			var comic = await _context.Comics.FirstOrDefaultAsync(c => c.Code == comicCode);
			if (comic == null)
			{
				return BadRequest("Comic not found");
			}

			var fieldDef = await _context.CrowdSourcedFieldDefinitions.FirstOrDefaultAsync(csfd => csfd.Code == fieldCode);
			if (fieldDef == null)
			{
				return BadRequest("Field not found");
			}

			Models.CrowdSourcedFieldDefinitionFeedback feedback = await _context
				.CrowdSourcedFieldDefinitionFeedbacks
				.Where(csfdf => csfdf.ComicId == comic.Id && csfdf.CrowdSourcedFieldDefinitionId == fieldDef.Id && csfdf.Id == id)
				.FirstOrDefaultAsync();
			
			feedback.CompletedBy = userId;
			feedback.CompletionComment = vm.CompletionComment;
			feedback.CompletionType = vm.CompletionType;
			feedback.CompletionDate = DateTimeOffset.Now;
			await _context.SaveChangesAsync();
			
			return Ok();
		}
	}
}
