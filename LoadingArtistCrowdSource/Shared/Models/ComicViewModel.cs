using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Code { get; set; } = "";

		[Required]
		[Url]
		public string Permalink { get; set; } = "";

		public DateTimeOffset ComicPublishedDate { get; set; }

		[Required]
		public string Title { get; set; } = "";

		public string? Tooltip { get; set; } = "";

		public string? Description { get; set; } = "";

		[Required]
		[Url]
		public string ImageUrlSrc { get; set; } = "";

		[Required]
		[Url]
		public string ImageThumbnailUrlSrc { get; set; } = "";

		[Required]
		[Url]
		public string? ImageWideThumbnailUrlSrc { get; set; }

		public DateTimeOffset ImportedDate { get; set; }

		public DateTimeOffset? LastUpdatedDate { get; set; }


		public ApplicationUserViewModel? ImportedByUser { get; set; }
		public ApplicationUserViewModel? LastUpdatedByUser { get; set; }
		public List<ComicHistoryLogViewModel> ComicHistoryLogs { get; set; } = new List<ComicHistoryLogViewModel>();
		public List<ComicFieldViewModel> ComicFields { get; set; } = new List<ComicFieldViewModel>();
		public TranscriptViewModel Transcript { get; set; } = new TranscriptViewModel();
		public ComicTagsViewModel Tags { get; set; } = new ComicTagsViewModel();
	}
}
