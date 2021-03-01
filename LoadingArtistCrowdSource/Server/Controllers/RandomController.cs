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
	[Route("api/random")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class RandomController : Controller
	{
		private readonly ApplicationDbContext _context;

		public RandomController(ApplicationDbContext context)
		{
			this._context = context;
		}

		[HttpGet]
		public async Task<string> Index()
		{
			var latestComic = await _context.Comics.OrderBy(c => c.Id).LastAsync();
			int randomId = System.Security.Cryptography.RandomNumberGenerator.GetInt32(1, latestComic.Id);

			var randomComic = await _context.Comics.Where(c => c.Id == randomId).FirstAsync();

			return randomComic.Code;
		}
	}
}
