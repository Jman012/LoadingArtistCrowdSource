using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class CrowdSourcedFieldVerifiedEntryViewModel
	{
		public DateTimeOffset VerificationDate { get; set; }
		public List<string> Values { get; set; } = new List<string>();

		public CrowdSourcedFieldDefinitionViewModel CrowdSourcedFieldDefinition { get; set; } = new CrowdSourcedFieldDefinitionViewModel();
		public ApplicationUserViewModel FirstCreatedByUser { get; set; } = new ApplicationUserViewModel();
	}
}
