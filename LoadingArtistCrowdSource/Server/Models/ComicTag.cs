using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Represents one tag for a comic.
	/// </summary>
	[Table(nameof(ComicTag))]
	public class ComicTag
	{
		/// <summary>
		/// The comic's Id this tag log is for.
		/// </summary>
		public int ComicId { get; set; }

		/// <summary>
		/// The value of the tag.
		/// </summary>
		public string Value { get; set; } = "";

		/// <summary>
		/// The id of the user who created this tag.
		/// </summary>
		public string? CreatedBy { get; set; } = "";

		/// <summary>
		/// The creation date of this tag.
		/// </summary>
		public DateTimeOffset CreatedDate { get; set; }

		public Comic Comic { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
	}
}
