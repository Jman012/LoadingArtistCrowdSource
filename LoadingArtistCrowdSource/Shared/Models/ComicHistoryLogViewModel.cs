using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicHistoryLogViewModel
	{
		public string ComicTitle { get; set; } = "";
		public ComicHistoryLogItemViewModel[] LogItems { get; set; } = new ComicHistoryLogItemViewModel[] { };
	}

	public class ComicHistoryLogItemViewModel
	{
		public int Id { get; set; }
		public string? FieldName { get; set; }
		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();
		public DateTimeOffset LogDate { get; set; }
		public string LogMessage { get; set; } = "";
		public string? OldValue { get; set; }
		public string? NewValue { get; set; }
	}
}
