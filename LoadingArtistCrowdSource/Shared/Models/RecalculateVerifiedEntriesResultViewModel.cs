using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class RecalculateVerifiedEntriesResultViewModel
	{
		public int CountVerified { get; set; }
		public int CountUnverified { get; set; }
		public int CountUnchanged { get; set; }
	}
}
