using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class FeedbackViewModel
	{
		[DisplayName("Comic Code")]
		public string ComicCode { get; set; } = "";
		[DisplayName("Field Code")]
		public string FieldCode { get; set; } = "";
		public int Id { get; set; }

		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();

		public DateTimeOffset CreatedDate { get; set; }

		[Required]
		[Description("Is the field missing an option required for this comic? Is the field description in need of improvement?")]
		public string Comment { get; set; } = "";

		public DateTimeOffset? CompletionDate { get; set; }

		public ApplicationUserViewModel? CompletedByUser { get; set; }

		public CrowdSourcedFieldDefinitionFeedbackCompletion? CompletionType { get; set; }

		public string? CompletionComment { get; set; }
	}
}
