using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class SearchViewModel
	{
		[DisplayName("Number")]
		public int? Id { get; set; }
		public string? Code { get; set; }
		public string? Title { get; set; }
		public string? Tooltip { get; set; }
		public string? Description { get; set; }
		public string? Transcript { get; set; }
		public SearchEntryViewModel[] SearchEntries { get; set; } = new SearchEntryViewModel[] { };
	}

	public class SearchEntryViewModel
	{
		public string FieldCode { get; set; } = "";
		public SearchEntryOperator Operator { get; set; } = SearchEntryOperator.Any;
		public SearchEntryOptionViewModel[] FieldValues { get; set; } = new SearchEntryOptionViewModel[] { };
	}

	public class SearchEntryOptionViewModel
	{
		public string Code { get; set; } = "";
		public bool Filtered { get; set; }
	}
}