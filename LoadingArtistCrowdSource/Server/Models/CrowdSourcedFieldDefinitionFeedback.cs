using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Records an instance of user feedback for a <see cref="CrowdSourcedFieldDefinition"/>.
	/// Such as missing an appropriate value, or an issue with 
	/// </summary>
	[Table(nameof(CrowdSourcedFieldDefinitionFeedback))]
	public class CrowdSourcedFieldDefinitionFeedback
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
		/// The user that created this feedback.
		/// </summary>
		public string CreatedBy { get; set; } = "";

		/// <summary>
		/// The date and time this feedback was created
		/// </summary>
		public DateTimeOffset CreatedDate { get; set; }

		/// <summary>
		/// The user's feedback.
		/// </summary>
		public string Comment { get; set; } = "";

		/// <summary>
		/// The date and time the feedback was acknowledged, completed, ignored, etc.
		/// </summary>
		public DateTimeOffset? CompletionDate { get; set; }

		/// <summary>
		/// The user that completed the feedback.
		/// </summary>
		public string? CompletedBy { get; set; }

		/// <summary>
		/// The type of completion for this feedback, once completed.
		/// </summary>
		public CrowdSourcedFieldDefinitionFeedbackCompletion? CompletionType { get; set; }

		/// <summary>
		/// A comment by the administrator who completed the feedback inquiry.
		/// </summary>
		public string? CompletionComment { get; set; }


		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
		public ApplicationUser CompletedByUser { get; set; } = null!;
	}
}
