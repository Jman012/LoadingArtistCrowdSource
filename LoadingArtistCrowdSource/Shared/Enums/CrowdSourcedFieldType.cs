using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Enums
{
	public enum CrowdSourcedFieldType
	{
		[Description("Section")]
		Section,

		[Description("Checkbox")]
		Checkbox,

		[Description("Checkboxes")]
		Checkboxes,

		[Description("Integer Number")]
		IntegerNumber,

		[Description("Freeform Textfield")]
		FreeformTextfield,

		[Description("Freeform Textarea")]
		FreeformTextarea,

		[Description("Dropdown")]
		Dropdown,

		[Description("Multi Dropdown")]
		MultiDropdown,

		[Description("Radio Buttons")]
		RadioButtons,
	}
}
