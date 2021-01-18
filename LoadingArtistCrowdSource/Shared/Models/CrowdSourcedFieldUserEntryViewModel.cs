using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class CrowdSourcedFieldUserEntryViewModel
	{
		public DateTimeOffset CreatedDate { get; set; }
		public List<string> Values { get; set; } = new List<string>();

		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();
	}
}
