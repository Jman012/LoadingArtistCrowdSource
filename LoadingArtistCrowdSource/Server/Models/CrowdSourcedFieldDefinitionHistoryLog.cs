using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	[Table(nameof(CrowdSourcedFieldDefinitionHistoryLog))]
	public class CrowdSourcedFieldDefinitionHistoryLog
	{
		/// <summary>
		/// The field's Id this feedback is for.
		/// </summary>
		public Guid CrowdSourcedFieldDefinitionId { get; set; }

		/// <summary>
		/// Sequential Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The user's Id this history log is for.
		/// </summary>
		public string? CreatedBy { get; set; } = "";

		/// <summary>
		/// The creation date of this history log.
		/// </summary>
		public DateTimeOffset LogDate { get; set; }

		/// <summary>
		/// A description of the history log.
		/// </summary>
		public string LogMessage { get; set; } = "";

		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
	}
}
