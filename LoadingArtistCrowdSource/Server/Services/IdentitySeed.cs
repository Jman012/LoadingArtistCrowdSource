using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using LoadingArtistCrowdSource.Server.Models;
using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Server.Services
{
	public static class IdentitySeed
	{
		public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			SeedUsers(userManager);
			SeedRoles(roleManager, userManager);
		}

		public static void SeedUsers(UserManager<ApplicationUser> userManager)
		{
		}

		public static void SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			if (!roleManager.RoleExistsAsync(Roles.Administrator).Result)
			{
				roleManager.CreateAsync(new IdentityRole()
				{
					Name = Roles.Administrator,
				}).Wait();

				var admin = userManager.FindByNameAsync("jman012guy@gmail.com").Result;
				userManager.AddToRoleAsync(admin, Roles.Administrator).Wait();
			}

			if (!roleManager.RoleExistsAsync(Roles.Moderator).Result)
			{
				roleManager.CreateAsync(new IdentityRole()
				{
					Name = Roles.Moderator,
				}).Wait();
			}
		}
	}
}
