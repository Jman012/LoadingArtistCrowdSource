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
			// Transcript = 0 or 1 points.
			// Tags = 0 or 1 points.
			// Which is implemented by: (CountUserEntries + CountVerifiedEntries) / (TotalCountFields * 2)
			// Ignore fields where type is Section.

			// Get year range
			var firstComic = await _context.Comics.OrderBy(c => c.Id).FirstAsync();
			var lastComic = await _context.Comics.OrderByDescending(c => c.Id).FirstAsync();
			var allYears = Enumerable.Range(firstComic.ComicPublishedDate.Year, lastComic.ComicPublishedDate.Year - firstComic.ComicPublishedDate.Year + 1);
			_logger.LogInformation($"{firstComic.ComicPublishedDate.Year}, {lastComic.ComicPublishedDate.Year}");

			// Collect data by year
			var userEntryCountsByYear = new Dictionary<int, int>();
			var verifiedEntryCountsByYear = new Dictionary<int, int>();
			var comicTranscriptCountsByYear = new Dictionary<int, int>();
			var comicTagCountsByYear = new Dictionary<int, int>();
			var comicCountsByYear = new Dictionary<int, int>();
			foreach (var year in allYears)
			{
				var userEntryCount = await _context.CrowdSourcedFieldUserEntries
					.Where(csfue => csfue.Comic.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && csfue.Comic.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
				var verifiedEntryCount = await _context.CrowdSourcedFieldVerifiedEntries
					.Where(csfve => csfve.Comic.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && csfve.Comic.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
				var transcriptCount = await _context.ComicTranscripts
					.Where(ct => ct.Comic.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && ct.Comic.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
				var tagCount = await _context.ComicTags
					.Where(ct => ct.Comic.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && ct.Comic.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.GroupBy(ct => ct.ComicId)
					.CountAsync();
				var comicCount = await _context.Comics
					.Where(c => c.ComicPublishedDate >= EF.Functions.DateFromParts(year, 1, 1) && c.ComicPublishedDate < EF.Functions.DateFromParts(year + 1, 1, 1))
					.CountAsync();
					
				userEntryCountsByYear.Add(year, userEntryCount);
				verifiedEntryCountsByYear.Add(year, verifiedEntryCount);
				comicTranscriptCountsByYear.Add(year, transcriptCount);
				comicTagCountsByYear.Add(year, tagCount);
				comicCountsByYear.Add(year, comicCount);
			}

			// Get field information
			int totalCountFieldsNotSection = await _context.CrowdSourcedFieldDefinitions
				.Where(csfd => csfd.Type != CrowdSourcedFieldType.Section)
				.CountAsync();
			int totalFieldPoints = totalCountFieldsNotSection * 2;

			int totalPerComicPoints = totalFieldPoints
				+ 1 // 1 point per Transcript
				+ 1; // 1 point per group of Tags
			if (totalPerComicPoints == 0)
			{
				return new StatisticsViewModel();
			}

			var vm = new StatisticsViewModel();

			int totalAccruedPoints = 0;
			foreach (var year in allYears)
			{
				int accruedPoints = userEntryCountsByYear[year]
					+ verifiedEntryCountsByYear[year]
					+ comicTranscriptCountsByYear[year]
					+ comicTagCountsByYear[year];
				double integrity = (double)accruedPoints / ((double)totalPerComicPoints * (double)comicCountsByYear[year]);
				vm.IntegrityByYear.Add(year, integrity);

				totalAccruedPoints += accruedPoints;
			}

			int totalComicCount = comicCountsByYear.Values.Sum();
			vm.OverallIntegrity = (double)totalAccruedPoints / ((double)totalPerComicPoints * (double)totalComicCount);

			return vm;
		}
	}
}
