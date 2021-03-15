using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Represents a single, published comic and some metadata information
	/// describing it.
	/// </summary>
	[Table(nameof(Comic))]
	public class Comic
	{
		/// <summary>
		/// A sequential Id of the published comic, in order of time of publication.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The unqiue code for the comic. This is typically the URL path for the comic.
		/// </summary>
		public string Code { get; set; } = "";

		/// <summary>
		/// A permalink to the comic's website of origination.
		/// This would be the URL a user normally uses to visit the website and view
		/// this specific comic.
		/// </summary>
		public string Permalink { get; set; } = "";

		/// <summary>
		/// The time the comic was published publicly.
		/// If there is no time information, it is assumed it was published
		/// at 12:00:00 AM of the date.
		/// </summary>
		public DateTimeOffset ComicPublishedDate { get; set; }

		/// <summary>
		/// The official title of the published comic.
		/// </summary>
		public string Title { get; set; } = "";

		/// <summary>
		/// If available, the tooltip of the comic.
		/// Otherwise known as "alt text" or "title text".
		/// </summary>
		public string? Tooltip { get; set; } = "";

		/// <summary>
		/// If available, a description that accompanies the published comic.
		/// Stored in HTML.
		/// </summary>
		public string? Description { get; set; } = "";

		/// <summary>
		/// A URL to the raw image of the comic.
		/// If multiple images are used to create the comic, or there is no
		/// single image to use, then the first shall be used.
		/// </summary>
		public string ImageUrlSrc { get; set; } = "";

		/// <summary>
		/// A URL to the thumbnail of the comic.
		/// </summary>
		public string ImageThumbnailUrlSrc { get; set; } = "";

		/// <summary>
		/// A URL to the wide version of the thumbnail of the comic.
		/// This may not always be present.
		/// </summary>
		public string? ImageWideThumbnailUrlSrc { get; set; }

		/// <summary>
		/// The time at which this published comic was imported to the database.
		/// </summary>
		public DateTimeOffset ImportedDate { get; set; }

		/// <summary>
		/// The user that initiated the import of this comic to the database.
		/// </summary>
		public string ImportedBy { get; set; } = "";

		/// <summary>
		/// The time at which any information about this comic was last updated.
		/// </summary>
		public DateTimeOffset? LastUpdatedDate { get; set; }

		/// <summary>
		/// The user that last updated the information about this comic.
		/// </summary>
		public string? LastUpdatedBy { get; set; }


		public ApplicationUser ImportedByUser { get; set; } = null!;
		public ApplicationUser? LastUpdatedByUser { get; set; }
		public List<ComicHistoryLog> ComicHistoryLogs { get; set; } = null!;
		public ComicTranscript? ComicTranscript { get; set; }
		public List<ComicTranscriptHistory> ComicTranscriptHistories { get; set; } = null!;
		public List<CrowdSourcedFieldDefinitionFeedback> FieldFeedbacks { get; set; } = null!;
		public List<CrowdSourcedFieldUserEntry> CrowdSourcedFieldUserEntries { get; set; } = null!;
		public List<CrowdSourcedFieldUserEntryValue> CrowdSourcedFieldUserEntryValues { get; set; } = null!;
		public List<CrowdSourcedFieldVerifiedEntry> CrowdSourcedFieldVerifiedEntries { get; set; } = null!;
		public List<CrowdSourcedFieldVerifiedEntryValue> CrowdSourcedFieldVerifiedEntryValues { get; set; } = null!;
	}
}
