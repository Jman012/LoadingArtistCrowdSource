using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class FieldDefinitionFormViewModel
	{
		[DisplayName("Code")]
		[Description("A URL-friendly identifier derived from the Name.")]
		[Required]
		[StringLength(120)]
		public string Code { get; set; } = "";

		[DisplayName("Name")]
		[Description("The displayable name for this field.")]
		[Required]
		[StringLength(100)]
		public string Name { get; set; } = "";

		[DisplayName("Active")]
		[Description("Inactive fields will not appear for regular users.")]
		public bool IsActive { get; set; }

		[DisplayName("Field Type")]
		[Description("Determines the data input method for this field.")]
		[Required]
		public CrowdSourcedFieldType Type { get; set; }

		[DisplayName("Short Description")]
		[Description("Provides a short description for the user to remember what the field should be answering, if the Name is too terse. Shouldn't be more than one or two sentences.")]
		[Required]
		[StringLength(10000)]
		public string ShortDescription { get; set; } = "";

		[DisplayName("Long Description")]
		[Description("Provides a detailed description of this field, with examples or other rules that the user should keep in mind when answering.")]
		[Required]
		[StringLength(10000)]
		public string LongDescription { get; set; } = "";

		[ReadOnly(true)]
		public DateTimeOffset CreatedDate { get; set; }

		[ReadOnly(true)]
		public string CreatedBy { get; set; } = "";

		[ReadOnly(true)]
		public DateTimeOffset? LastUpdatedDate { get; set; }

		[ReadOnly(true)]
		public string? LastUpdatedBy { get; set; }
	}
}
