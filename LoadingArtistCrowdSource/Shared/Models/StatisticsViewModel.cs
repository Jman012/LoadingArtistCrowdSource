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
		public double OverallIntegrity { get; set; }
		public Dictionary<int, double> IntegrityByYear { get; set; } = new Dictionary<int, double>();
	}
}
