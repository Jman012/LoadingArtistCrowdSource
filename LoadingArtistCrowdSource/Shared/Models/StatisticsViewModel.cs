using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class StatisticsViewModel
	{
		public int OverallIntegrity { get; set; }
		public Dictionary<int, int> IntegrityByYear { get; set; } = new Dictionary<int, int>();
	}
}
