using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicPageViewModel
	{
		public ComicViewModel ComicViewModel { get; set; } = new ComicViewModel();
		public string? FirstComicCode { get; set; }
		public string? PreviousComicCode { get; set; }
		public string? NextComicCode { get; set; }
		public string? LatestComicCode { get; set; }
		public double Progress { get; set; }
	}
}
