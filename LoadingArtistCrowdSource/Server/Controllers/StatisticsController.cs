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
	[Route("api/statistics")]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class StatisticsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<StatisticsController> _logger;

		public StatisticsController(ApplicationDbContext context, ILogger<StatisticsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<StatisticsViewModel> Index()
		{
			// Each field = 0, 1, or 2 points.
			// Verified = 2 points, Collecting = 1 points, No Data = 0 points
			// Which is implemented by: (CountUserEntries + CountVerifiedEntries) / (TotalCountFields * 2)
			// Ignore fields where type is Section.

			var firstComic = await _context.Comics.OrderBy(c => c.Id).FirstAsync();
			var lastComic = await _context.Comics.OrderByDescending(c => c.Id).FirstAsync();
			var allYears = Enumerable.Range(firstComic.ComicPublishedDate.Year, lastComic.ComicPublishedDate.Year - firstComic.ComicPublishedDate.Year + 1);
			_logger.LogInformation($"{firstComic.ComicPublishedDate.Year}, {lastComic.ComicPublishedDate.Year}");

			var userEntryCountsByYear = new Dictionary<int, int>();
			var verifiedEntryCountsByYear = new Dictionary<int, int>();
			var comicCountsByYear = new Dictionary<int, int>();
			foreach (var year in allYears)
			{
				var userEntryCount = await _context.CrowdSourcedFieldUserEntries
					.Where(csfue => csfue.Comic.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && csfue.Comic.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
				var verifiedEntryCount = await _context.CrowdSourcedFieldVerifiedEntries
					.Where(csfve => csfve.Comic.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && csfve.Comic.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
				var comicCount = await _context.Comics
					.Where(c => c.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && c.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
					
				userEntryCountsByYear.Add(year, userEntryCount);
				verifiedEntryCountsByYear.Add(year, verifiedEntryCount);
				comicCountsByYear.Add(year, comicCount);
			}

			int totalCountFieldsNotSection = await _context.CrowdSourcedFieldDefinitions
				.Where(csfd => csfd.Type != CrowdSourcedFieldType.Section)
				.CountAsync();
			int totalFieldPoints = totalCountFieldsNotSection * 2;

			var vm = new StatisticsViewModel();

			int totalAccruedPoints = 0;
			foreach (var year in allYears)
			{
				int accruedPoints = userEntryCountsByYear[year] + verifiedEntryCountsByYear[year];
				double integrity = (double)accruedPoints / ((double)totalFieldPoints * (double)comicCountsByYear[year]);
				vm.IntegrityByYear.Add(year, integrity);

				totalAccruedPoints += accruedPoints;
			}

			int totalComicCount = comicCountsByYear.Values.Sum();
			vm.OverallIntegrity = (double)totalAccruedPoints / ((double)totalFieldPoints * (double)totalComicCount);

			return vm;
		}
	}
}
