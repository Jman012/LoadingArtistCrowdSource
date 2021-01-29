using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicFieldViewModel
	{
		public string Code { get; set; } = "";
		public CrowdSourcedFieldType Type { get; set; }
		public int DisplayOrder { get; set; }
		public string Name { get; set; } = "";
		public string ShortDescription { get; set; } = "";
		public string LongDescription { get; set; } = "";
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset? LastUpdatedDate { get; set; }

		public List<CrowdSourcedFieldDefinitionOptionViewModel> Options { get; set; } = new List<CrowdSourcedFieldDefinitionOptionViewModel>();
		public List<CrowdSourcedFieldUserEntryViewModel> UserEntries { get; set; } = new List<CrowdSourcedFieldUserEntryViewModel>();
		public CrowdSourcedFieldVerifiedEntryViewModel? VerifiedEntry { get; set; }

	}
}
