using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class TranscriptViewModel
	{
		public ApplicationUserViewModel LastEditedByUser { get; set; } = new ApplicationUserViewModel();

		public DateTimeOffset LastEditedDate { get; set; }

		[Required]
		[DisplayName("Transcript Contents")]
		[Description("The transcript should contain all legible text present in the comic, with no additional text except for the \"LoadingArtist.com\" watermark.")]
		public string TranscriptContent { get; set; } = "";
	}
}