using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Used to represent one or more values for a <see cref="CrowdSourcedFieldUserEntry"/>.
	/// Most entries will only have a single value, but multi-selects or similar may have more than one,
	/// and we don't want to store it all in a single column.
	/// </summary>
	/// <remarks>
	/// The key of this table is {ComicId, CrowdSourcedFieldDefinitionId, CreatedBy, Id}, 
	/// where {ComicId, CrowdSourcedFieldDefinitionId, CreatedBy} is a foreign key to <see cref="CrowdSourcedFieldUserEntry"/>,
	/// and {Id} identifies this Value among multiple.
	/// This means that there can be multiple values to an entry, matching the entry by the comic/field/user.
	/// </remarks>
	[Table(nameof(CrowdSourcedFieldUserEntryValue))]
	public class CrowdSourcedFieldUserEntryValue
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
		/// The user's Id this value is for.
		/// </summary>
		public string CreatedBy { get; set; } = "";

		/// <summary>
		/// The unique Id for this comic's field's user's entry value.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The value data for this object. Stored as a string for flexibility
		/// among all value types.
		/// </summary>
		public string Value { get; set; } = "";



		public Comic Comic { get; set; } = null!;
		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
		public ApplicationUser CreatedByUser { get; set; } = null!;
		public CrowdSourcedFieldUserEntry CrowdSourcedFieldUserEntry { get; set; } = null!;
	}
}
