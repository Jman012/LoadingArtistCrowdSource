using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class CrowdSourcedFieldDefinitionHistoryLogViewModel
	{
		public string FieldTitle { get; set; } = "";
		public CrowdSourcedFieldDefinitionHistoryLogItemViewModel[] LogItems { get; set; } = new CrowdSourcedFieldDefinitionHistoryLogItemViewModel[] { };
	}

	public class CrowdSourcedFieldDefinitionHistoryLogItemViewModel
	{
		public int Id { get; set; }
		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();
		public DateTimeOffset LogDate { get; set; }
		public string LogMessage { get; set; } = "";
		public string? OldValue { get; set; }
		public string? NewValue { get; set; }
	}
}
