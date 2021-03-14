using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Represents the history of transcript contributions and edits for a comic.
	/// </summary>
	[Table(nameof(ComicTranscriptHistory))]
	public class ComicTranscriptHistory
	{
		/// <summary>
		/// The id of the comic this transcript belongs to.
		/// </summary>
		public int ComicId { get; set; }

		/// <summary>
		/// The id of the history entry for the comic's transcript.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The id of the user who created this transcript version.
		/// </summary>
		public string CreatedBy { get; set; } = "";

		/// <summary>
		/// The date and time the user submitted this transcript version.
		/// </summary>
		public DateTimeOffset CreatedDate { get; set; }

		/// <summary>
		/// The content of the user's transcript.
		/// </summary>
		public string TranscriptContent { get; set; } = "";

		/// <summary>
		/// The pre-rendered diff of this transcript and the previous one.
		/// </summary>
		public string DiffWithPrevious { get; set; } = "";

		public Comic Comic { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
	}
}