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
using Microsoft.Extensions.Caching.Distributed;

namespace LoadingArtistCrowdSource.Server.Controllers
{
	[ApiController]
	[Route("api/tag")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class TagController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly Services.TagRepository _tagRepo;
		private readonly Services.JsonDistributedCache<ComicController> _distCache;

		public TagController(
			ApplicationDbContext context, 
			Services.TagRepository tagRepo,
			Services.JsonDistributedCache<ComicController> distCache)
		{
			_context = context;
			_tagRepo = tagRepo;
			_distCache = distCache;
		}

		[HttpGet]
		public async Task<ComicTagsViewModel> Index()
		{
			// Attempt to fulfill request from cache
			var result = await _distCache.GetAsync<ComicTagsViewModel>(Services.CacheKeys.LACS.Tags.Index);
			if (result != null)
			{
				return result;
			}

			Services.ModelMapper modelMapper = new Services.ModelMapper();
			List<ComicTagViewModel> uniqueTags = await _tagRepo.GetSystemTags();

			var vm = new ComicTagsViewModel()
			{
				TagValues = uniqueTags,
			};

			// Cache results for later
			await _distCache.SetAsync(Services.CacheKeys.LACS.Tags.Index, vm, new DistributedCacheEntryOptions()
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
			});

			return vm;
		}
	}
}
