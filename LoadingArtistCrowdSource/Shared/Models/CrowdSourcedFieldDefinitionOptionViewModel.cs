using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class CrowdSourcedFieldDefinitionOptionViewModel
	{
		[Required]
		[DisplayName("Code")]
		[Description("A unique identifer among the options for this field. Changing this value on fields that have already been answered by users will cause disruptions to answers.")]
		public string Code { get; set; } = "";
		[Required]
		public string Text { get; set; } = "";
		[Required]
		public string Description { get; set; } = "";
		[Url]
		public string? URL { get; set; }
	}
}
