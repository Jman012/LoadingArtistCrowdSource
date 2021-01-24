using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class CrowdSourcedFieldDefinitionViewModel
	{
		public Guid Id { get; set; }
		public bool IsActive { get; set; } = true;
		public bool IsDeleted { get; set; } = false;
		public CrowdSourcedFieldType Type { get; set; }
		public int DisplayOrder { get; set; }
		public string Name { get; set; } = "";
		public string ShortDescription { get; set; } = "";
		public string LongDescription { get; set; } = "";
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset? LastUpdatedDate { get; set; }

		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();
		public ApplicationUserViewModel LastUpdatedByUser { get; set; } = new ApplicationUserViewModel();

	}
}
