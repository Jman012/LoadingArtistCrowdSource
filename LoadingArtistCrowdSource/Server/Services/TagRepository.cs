using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Shared.Models;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class TagRepository
	{
		private readonly ApplicationDbContext _context;

		public TagRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<ComicTagViewModel>> GetSystemTags()
		{
			return (await _context
				.ComicTags
				.GroupBy(ct => ct.Value)
				.Select(ctg => new { Value = ctg.Key, Count = ctg.Count() })
				.OrderBy(ctg => ctg.Value)
				.ToListAsync())
				.Select(ctg => new ComicTagViewModel()
				{
					TagValue = ctg.Value,
					TagSystemCount = ctg.Count,
				})
				.ToList();
		}

		public void SetSystemTagCounts(ComicViewModel comicViewModel, List<ComicTagViewModel> uniqueTags)
		{
			Dictionary<string, ComicTagViewModel> dctTags = uniqueTags.ToDictionary(t => t.TagValue);
			foreach (var comicTag in comicViewModel.Tags.TagValues)
			{
				ComicTagViewModel tag;
				if (dctTags.TryGetValue(comicTag.TagValue, out tag!))
				{
					comicTag.TagSystemCount = tag.TagSystemCount;
				}
			}
		}
	}
}