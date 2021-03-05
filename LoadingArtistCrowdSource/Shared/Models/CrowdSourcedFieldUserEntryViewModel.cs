using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Models
{
	[CrowdSourcedFieldUserEntryViewModelValidation]
	public class CrowdSourcedFieldUserEntryViewModel
	{
		public DateTimeOffset CreatedDate { get; set; }
		public List<string> Values { get; set; } = new List<string>();

		public ApplicationUserViewModel CreatedByUser { get; set; } = new ApplicationUserViewModel();
	}

	public class CrowdSourcedFieldUserEntryViewModelValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			CrowdSourcedFieldUserEntryViewModel? vm = value as CrowdSourcedFieldUserEntryViewModel;
			if (vm == null)
			{
				return new ValidationResult($"The {nameof(CrowdSourcedFieldUserEntryViewModelValidationAttribute)} was not applied to an instance of a {nameof(CrowdSourcedFieldUserEntryViewModel)}.");
			}

			if (vm.Values == null || !vm.Values.Any() || vm.Values.All(string.IsNullOrEmpty))
			{
				return new ValidationResult("At least one value must be chosen");
			}

			return null;
		}
	}
}
