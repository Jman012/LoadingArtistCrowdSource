using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class ComicTagsViewModel
	{
		[Required]
		public List<string> TagValues { get; set; } = new List<string>();
		public List<ApplicationUserViewModel> Contributors { get; set; } = new List<ApplicationUserViewModel>();
	}
}
