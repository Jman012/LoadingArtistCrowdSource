using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// A user's entry of a field to a comic.
	/// </summary>
	/// <remarks>
	/// The key of this table is {ComicId, CrowdSourcedFieldDefinitionId, CreatedBy}, meaning that
	/// there can only be one entry per user for a field on a comic.
	/// </remarks>
	[Table(nameof(CrowdSourcedFieldUserEntry))]
	public class CrowdSourcedFieldUserEntry
	{
		/// <summary>
		/// The comic's Id this entry is for.
		/// </summary>
		public int ComicId { get; set; }

		/// <summary>
		/// The field's Id this entry is for.
		/// </summary>
		public Guid CrowdSourcedFieldDefinitionId { get; set; }

		/// <summary>
		/// The user that created this entry.
		/// </summary>
		public string CreatedBy { get; set; } = "";

		/// <summary>
		/// The time the user created this entry.
		/// </summary>
		public DateTimeOffset CreatedDate { get; set; }

		/// <summary>
		/// The time the user last updated this entry.
		/// This is also updated when the <see cref="CrowdSourcedFieldUserEntryValue"/>s
		/// are updated.
		/// </summary>
		public DateTimeOffset LastUpdatedDate { get; set; }


		public Comic Comic { get; set; } = null!;
		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
		public List<CrowdSourcedFieldUserEntryValue> CrowdSourcedFieldUserEntryValues { get; set; } = null!;
	}
}
