using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicTagViewModel
	{
		[Required]
		[RegularExpression(@"^[\p{L}\p{M}\p{N}\-_ ]+$", ErrorMessage = "Invalid characters. Keep it simple!")]
		public string TagValue { get; set; } = "";

		public int TagSystemCount { get; set; }

		public static string Transform(string value) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
	}
	public class ComicTagsViewModel
	{
		[Required]
		public List<ComicTagViewModel> TagValues { get; set; } = new List<ComicTagViewModel>();
		public List<ApplicationUserViewModel> Contributors { get; set; } = new List<ApplicationUserViewModel>();
	}
}
