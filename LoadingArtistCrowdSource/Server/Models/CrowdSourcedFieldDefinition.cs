using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Defines a possible field for users to submit answers on a comic.
	/// </summary>
	[Table(nameof(CrowdSourcedFieldDefinition))]
	public class CrowdSourcedFieldDefinition
	{
		/// <summary>
		/// Random Id assigned upon creation.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Disable a field definition by setting IsActive to false.
		/// When inactive, will not appear on comics, but will still be 
		/// editable for administrators
		/// </summary>
		public bool IsActive { get; set; } = true;

		/// <summary>
		/// Delete a field from comics and from administration/configuration
		/// by setting this to true.
		/// </summary>
		public bool IsDeleted { get; set; } = false;

		/// <summary>
		/// The data type for the field. Determines what kind of values
		/// can be given for the field.
		/// </summary>
		public CrowdSourcedFieldType Type { get; set; }

		/// <summary>
		/// The display order of the field in the UI.
		/// </summary>
		public int DisplayOrder { get; set; }

		/// <summary>
		/// A short name for the field, typically display as a label for
		/// the field. Should be no more than a few words.
		/// </summary>
		public string Name { get; set; } = "";

		/// <summary>
		/// A short description to describe the field, which may be displayed
		/// as a sub-label for the field. Should be a sentence.
		/// </summary>
		public string ShortDescription { get; set; } = "";

		/// <summary>
		/// A longer description to describe the field, which shoudl not be
		/// displayed by default. It may be shown through an (i) button.
		/// Should be at least a few sentences, possibly with examples.
		/// </summary>
		public string LongDescription { get; set; } = "";

		/// <summary>
		/// The time at which this definition was first created.
		/// </summary>
		public DateTimeOffset CreatedDate { get; set; }

		/// <summary>
		/// The user that created the definition.
		/// </summary>
		public string CreatedBy { get; set; } = "";

		/// <summary>
		/// The time at which this definition was last updated.
		/// </summary>
		public DateTimeOffset? LastUpdatedDate { get; set; }

		/// <summary>
		/// The user that last updated the definition.
		/// </summary>
		public string? LastUpdatedBy { get; set; }

		// TODO: Validations


		public List<ComicHistoryLog> ComicHistoryLogs { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
		public ApplicationUser? LastUpdatedByUser { get; set; }
		public List<CrowdSourcedFieldDefinitionFeedback> CrowdSourcedFieldDefinitionFeedbacks { get; set; } = null!;
		public List<CrowdSourcedFieldDefinitionHistoryLog> CrowdSourcedFieldDefinitionHistoryLogs { get; set; } = null!;
		public List<CrowdSourcedFieldUserEntry> CrowdSourcedFieldUserEntries { get; set; } = null!;
		public List<CrowdSourcedFieldUserEntryValue> CrowdSourcedFieldUserEntryValues { get; set; } = null!;
		public List<CrowdSourcedFieldVerifiedEntry> CrowdSourcedFieldVerifiedEntries { get; set; } = null!;
		public List<CrowdSourcedFieldVerifiedEntryValue> CrowdSourcedFieldVerifiedEntryValues { get; set; } = null!;
	}
}
