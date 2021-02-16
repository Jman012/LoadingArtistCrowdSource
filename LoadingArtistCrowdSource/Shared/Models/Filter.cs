using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class Filter
	{
		public string FieldDefinitionCode { get; set; } = "";
		public FilterOperator Operator { get; set; }
		public KeyValuePair<string, string>[] Values { get; set; } = new KeyValuePair<string, string>[] { };
	}
}
