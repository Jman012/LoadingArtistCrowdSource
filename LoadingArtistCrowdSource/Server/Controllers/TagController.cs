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
	[Route("api/tag")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class TagController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly Services.TagRepository _tagRepo;

		public TagController(ApplicationDbContext context, Services.TagRepository tagRepo)
		{
			_context = context;
			_tagRepo = tagRepo;
		}

		[HttpGet]
		public async Task<ComicTagsViewModel> Index()
		{
			Services.ModelMapper modelMapper = new Services.ModelMapper();
			List<ComicTagViewModel> uniqueTags = await _tagRepo.GetSystemTags();

			return new ComicTagsViewModel()
			{
				TagValues = uniqueTags,
			};
		}
	}
}
