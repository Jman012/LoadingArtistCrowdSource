using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Represents the current transcript for a comic, with
	/// data about the last editor.
	/// </summary>
	[Table(nameof(ComicTranscript))]
	public class ComicTranscript
	{
		/// <summary>
		/// The id of the comic the transcript belongs to.
		/// </summary>
		public int ComicId { get; set; }

		/// <summary>
		/// The id of the user who last edited this comic's transcript.
		/// </summary>
		public string LastEditedBy { get; set; } = "";

		/// <summary>
		/// The date and time when the last user edited this comic's transcript.
		/// </summary>
		public DateTimeOffset LastEditedDate { get; set; }

		/// <summary>
		/// The current content of the transcript.
		/// </summary>
		public string TranscriptContent { get; set; } = "";

		public Comic Comic { get; set; } = null!;
		public ApplicationUser LastEditedByUser { get; set; } = null!;
	}
}