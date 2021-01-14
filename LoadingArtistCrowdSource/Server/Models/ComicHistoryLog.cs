using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Tracks the history of a Comic's history, concerning changing users' entries primarily.
	/// </summary>
	[Table(nameof(ComicHistoryLog))]
	public class ComicHistoryLog
	{
		/// <summary>
		/// The comic's Id this history log is for.
		/// </summary>
		public int ComicId { get; set; }

		/// <summary>
		/// The Id for this history log.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The field's Id this history log is for, if any.
		/// </summary>
		public Guid? CrowdSourcedFieldDefinitionId { get; set; }

		/// <summary>
		/// The user's Id this history log is for.
		/// </summary>
		public string? CreatedBy { get; set; } = "";

		/// <summary>
		/// The creation date of this history log.
		/// </summary>
		public DateTimeOffset LogDate { get; set; }

		/// <summary>
		/// If this history log is about a value change, the previous value.
		/// </summary>
		public string? OldValue { get; set; }

		/// <summary>
		/// If this history log is about a value change, the new value.
		/// </summary>
		public string? NewValue { get; set; }

		/// <summary>
		/// A description of the history log.
		/// </summary>
		public string LogMessage { get; set; } = "";

		public Comic Comic { get; set; } = null!;
		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
	}
}
