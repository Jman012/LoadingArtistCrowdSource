using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class AdminSetUserRolesViewModel
	{
		[Required]
		public string Username { get; set; } = "";

		public List<AdminSetUserRoleItemViewModel> UserRoles { get; set; } = new List<AdminSetUserRoleItemViewModel>();
	}

	public class AdminSetUserRoleItemViewModel
	{
		public string Role { get; set; } = "";
		public bool Include { get; set; }
	}
}