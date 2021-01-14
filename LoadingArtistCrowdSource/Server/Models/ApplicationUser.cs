using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace LoadingArtistCrowdSource.Server.Models
{
	public class ApplicationUser : IdentityUser
	{
		public List<Comic> ComicsImported { get; set; } = null!;
		public List<Comic> ComicsLastUpdated { get; set; } = null!;

		public List<ComicHistoryLog> ComicHistoryLogsCreated { get; set; } = null!;

		public List<CrowdSourcedFieldDefinition> CrowdSourcedFieldDefinitionsCreated { get; set; } = null!;
		public List<CrowdSourcedFieldDefinition> CrowdSourcedFieldDefinitionsLastUpdated { get; set; } = null!;

		public List<CrowdSourcedFieldDefinitionFeedback> CrowdSourcedFieldDefinitionFeedbacksCreated { get; set; } = null!;
		public List<CrowdSourcedFieldDefinitionFeedback> CrowdSourcedFieldDefinitionFeedbacksCompleted { get; set; } = null!;

		public List<CrowdSourcedFieldDefinitionHistoryLog> CrowdSourcedFieldDefinitionHistoryLogsCreated { get; set; } = null!;

		public List<CrowdSourcedFieldUserEntry> CrowdSourcedFieldUserEntriesCreated { get; set; } = null!;

		public List<CrowdSourcedFieldUserEntryValue> CrowdSourcedFieldUserEntryValuesCreated { get; set; } = null!;

		public List<CrowdSourcedFieldVerifiedEntry> CrowdSourcedFieldVerifiedEntriesFirstCreated { get; set; } = null!;
	}
}
