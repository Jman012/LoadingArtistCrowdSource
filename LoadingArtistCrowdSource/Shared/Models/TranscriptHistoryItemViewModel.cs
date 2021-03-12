using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class TranscriptHistoryItemViewModel
	{
		public int Id { get; set; }

		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();

		public DateTimeOffset CreatedDate { get; set; }

		[Required]
		public string TranscriptContent { get; set; } = "";

		public string DiffWithPrevious { get; set; } = "";
	}
}