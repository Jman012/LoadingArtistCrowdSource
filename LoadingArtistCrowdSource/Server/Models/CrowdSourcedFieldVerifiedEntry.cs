using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// A verified entry of a field to a comic.
	/// </summary>
	/// <remarks>
	/// The key of this table is {ComicId, CrowdSourcedFieldDefinitionId}, meaning that
	/// there can only be one entry for a field on a comic.
	/// </remarks>
	[Table(nameof(CrowdSourcedFieldVerifiedEntry))]
	public class CrowdSourcedFieldVerifiedEntry
	{
		/// <summary>
		/// The comic's Id this value is for.
		/// </summary>
		public int ComicId { get; set; }

		/// <summary>
		/// The field's Id this value is for.
		/// </summary>
		public Guid CrowdSourcedFieldDefinitionId { get; set; }

		/// <summary>
		/// The user's Id that first created an entry with a matching value.
		/// </summary>
		public string FirstCreatedBy { get; set; } = "";

		/// <summary>
		/// The date and time the verification was created.
		/// </summary>
		public DateTimeOffset VerificationDate { get; set; }


		public Comic Comic { get; set; } = null!;
		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
		public ApplicationUser FirstCreatedByUser { get; set; } = null!;
		public List<CrowdSourcedFieldVerifiedEntryValue> CrowdSourcedFieldVerifiedEntryValues { get; set; } = null!;
	}
}
