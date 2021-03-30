using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicListItemViewModel
	{
		public int Id { get; set; }

		public string Code { get; set; } = "";

		public DateTimeOffset ComicPublishedDate { get; set; }
		
		public string Title { get; set; } = "";

		public string ImageThumbnailUrlSrc { get; set; } = "";
		public int Integrity { get; set; }
	}
}
