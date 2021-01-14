﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.EntityFramework.Options;

using LoadingArtistCrowdSource.Server.Models;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoadingArtistCrowdSource.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			#region ApplicationUser
			
			#endregion ApplicationUser

			#region Comic
			// Keys
			builder.Entity<Comic>()
				.HasKey(c => c.Id);
			// Relationships
			builder.Entity<Comic>()
				.HasOne(c => c.ImportedByUser)
				.WithMany(au => au.ComicsImported)
				.HasForeignKey(c => c.ImportedBy)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Comic>()
				.HasOne(c => c.LastUpdatedByUser!)
				.WithMany(au => au.ComicsLastUpdated)
				.HasForeignKey(c => c.LastUpdatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion Comic

			#region ComicHistoryLog
			// Keys
			builder.Entity<ComicHistoryLog>()
				.HasKey(chl => new
				{
					chl.ComicId,
					chl.Id,
				});
			// Relationships
			builder.Entity<ComicHistoryLog>()
				.HasOne(chl => chl.Comic)
				.WithMany(c => c.ComicHistoryLogs)
				.HasForeignKey(chl => chl.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ComicHistoryLog>()
				.HasOne(chl => chl.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.ComicHistoryLogs)
				.HasForeignKey(chl => chl.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ComicHistoryLog>()
				.HasOne(chl => chl.CreatedByUser)
				.WithMany(au => au.ComicHistoryLogsCreated)
				.HasForeignKey(chl => chl.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion ComicHistoryLog

			#region CrowdSourcedFieldDefinition
			// Keys
			builder.Entity<CrowdSourcedFieldDefinition>()
				.HasKey(csfd => csfd.Id);
			// Relationships
			builder.Entity<CrowdSourcedFieldDefinition>()
				.HasOne(csfd => csfd.CreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldDefinitionsCreated)
				.HasForeignKey(csfd => csfd.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldDefinition>()
				.HasOne(csfd => csfd.LastUpdatedByUser)
				.WithMany(au => au.CrowdSourcedFieldDefinitionsLastUpdated)
				.HasForeignKey(csfd => csfd.LastUpdatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldDefinition

			#region CrowdSourcedFieldDefinitionFeedback
			// Keys
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.HasKey(csfdf => new
				{
					csfdf.CrowdSourcedFieldDefinitionId,
					csfdf.Id,
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.HasOne(csfdf => csfdf.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldDefinitionFeedbacks)
				.HasForeignKey(csfdf => csfdf.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.HasOne(csfdf => csfdf.CreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldDefinitionFeedbacksCreated)
				.HasForeignKey(csfdf => csfdf.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.HasOne(csfdf => csfdf.CompletedByUser)
				.WithMany(au => au.CrowdSourcedFieldDefinitionFeedbacksCompleted)
				.HasForeignKey(csfdf => csfdf.CompletedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldDefinitionFeedback

			#region CrowdSourcedFieldDefinitionHistoryLog
			// Keys
			builder.Entity<CrowdSourcedFieldDefinitionHistoryLog>()
				.HasKey(csfdfhl => new
				{
					csfdfhl.CrowdSourcedFieldDefinitionId,
					csfdfhl.Id,
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldDefinitionHistoryLog>()
				.HasOne(csfdfhl => csfdfhl.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldDefinitionHistoryLogs)
				.HasForeignKey(csfdfhl => csfdfhl.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldDefinitionHistoryLog>()
				.HasOne(csfdfhl => csfdfhl.CreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldDefinitionHistoryLogsCreated)
				.HasForeignKey(csfdfhl => csfdfhl.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldDefinitionHistoryLog

			#region CrowdSourcedFieldUserEntry
			// Keys
			builder.Entity<CrowdSourcedFieldUserEntry>()
				.HasKey(csfue => new
				{
					csfue.ComicId,
					csfue.CrowdSourcedFieldDefinitionId,
					csfue.CreatedBy
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldUserEntry>()
				.HasOne(csfue => csfue.Comic)
				.WithMany(c => c.CrowdSourcedFieldUserEntries)
				.HasForeignKey(csfue => csfue.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldUserEntry>()
				.HasOne(csfue => csfue.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldUserEntries)
				.HasForeignKey(csfue => csfue.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldUserEntry>()
				.HasOne(csfue => csfue.CreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldUserEntriesCreated)
				.HasForeignKey(csfue => csfue.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldUserEntry

			#region CrowdSourcedFieldUserEntryValue
			// Keys
			builder.Entity<CrowdSourcedFieldUserEntryValue>()
				.HasKey(csfuev => new
				{
					csfuev.ComicId,
					csfuev.CrowdSourcedFieldDefinitionId,
					csfuev.CreatedBy,
					csfuev.Id,
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldUserEntryValue>()
				.HasOne(csfuev => csfuev.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldUserEntryValues)
				.HasForeignKey(csfuev => csfuev.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldUserEntryValue>()
				.HasOne(csfuev => csfuev.CreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldUserEntryValuesCreated)
				.HasForeignKey(csfuev => csfuev.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldUserEntryValue>()
				.HasOne(csfuev => csfuev.CrowdSourcedFieldUserEntry)
				.WithMany(csfue => csfue.CrowdSourcedFieldUserEntryValues)
				.HasForeignKey(csfuev => new
				{
					csfuev.ComicId,
					csfuev.CrowdSourcedFieldDefinitionId,
					csfuev.CreatedBy,
				})
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldUserEntryValue

			#region CrowdSourcedFieldVerifiedEntry
			// Keys
			builder.Entity<CrowdSourcedFieldVerifiedEntry>()
				.HasKey(csfve => new
				{
					csfve.ComicId,
					csfve.CrowdSourcedFieldDefinitionId,
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldVerifiedEntry>()
				.HasOne(csfve => csfve.Comic)
				.WithMany(c => c.CrowdSourcedFieldVerifiedEntries)
				.HasForeignKey(csfve => csfve.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldVerifiedEntry>()
				.HasOne(csfve => csfve.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldVerifiedEntries)
				.HasForeignKey(csfve => csfve.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldVerifiedEntry>()
				.HasOne(csfve => csfve.FirstCreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldVerifiedEntriesFirstCreated)
				.HasForeignKey(csfve => csfve.FirstCreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldVerifiedEntry

			#region CrowdSourcedFieldVerifiedEntryValue
			// Keys
			builder.Entity<CrowdSourcedFieldVerifiedEntryValue>()
				.HasKey(csfvev => new
				{
					csfvev.ComicId,
					csfvev.CrowdSourcedFieldDefinitionId,
					csfvev.Id,
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldVerifiedEntryValue>()
				.HasOne(csfvev => csfvev.Comic)
				.WithMany(c => c.CrowdSourcedFieldVerifiedEntryValues)
				.HasForeignKey(csfvev => csfvev.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldVerifiedEntryValue>()
				.HasOne(csfvev => csfvev.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldVerifiedEntryValues)
				.HasForeignKey(csfvev => csfvev.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldVerifiedEntryValue>()
				.HasOne(csfvev => csfvev.CrowdSourcedFieldVerifiedEntry)
				.WithMany(csfve => csfve.CrowdSourcedFieldVerifiedEntryValues)
				.HasForeignKey(csfvev => new
				{
					csfvev.ComicId,
					csfvev.CrowdSourcedFieldDefinitionId,
				})
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldVerifiedEntryValue

			var adminUser = new ApplicationUser()
			{
				Id = "432ea055-ea01-443d-a6f7-e97d2c18d275",
				UserName = "jman012guy@gmail.com",
				NormalizedUserName = "JMAN012GUY@GMAIL.COM",
				Email = "jman012guy@gmail.com",
				NormalizedEmail = "JMAN012GUY@GMAIL.COM",
				EmailConfirmed = true,
				ConcurrencyStamp = "3acb17f1-65fe-4eac-bc2b-26403b23b999",
				AccessFailedCount = 0,
				LockoutEnabled = true,
				LockoutEnd = null,
				PasswordHash = "",
				PhoneNumber = "",
				PhoneNumberConfirmed = false,
				SecurityStamp = "",
				TwoFactorEnabled = false,
			};
			adminUser.PasswordHash = new Microsoft.AspNetCore.Identity.PasswordHasher<ApplicationUser>().HashPassword(adminUser, "Desert12!");
			builder.Entity<ApplicationUser>().HasData(adminUser);
			builder.Entity<Comic>().HasData(new Comic()
			{
				Id = 1,
				Permalink = "https://loadingartist.com/comic/born/",
				ComicPublishedDate = DateTimeOffset.Parse("2011-01-04T00:00:00.000-0800"),
				Title = "Born",
				Tooltip = "Born",
				Description = null,
				ImageUrlSrc = "https://loadingartist.com/wp-content/uploads/2011/07/2011-01-04-born.png",
				ImportedDate = DateTimeOffset.Now,
				ImportedBy = "432ea055-ea01-443d-a6f7-e97d2c18d275",
				LastUpdatedByUser = null,
				LastUpdatedBy = null,
			});
		}

		public DbSet<Comic> Comics => Set<Comic>();
		public DbSet<ComicHistoryLog> ComicHistoryLogs => Set<ComicHistoryLog>();
		public DbSet<CrowdSourcedFieldDefinition> CrowdSourcedFieldDefinitions => Set<CrowdSourcedFieldDefinition>();
		public DbSet<CrowdSourcedFieldDefinitionFeedback> CrowdSourcedFieldDefinitionFeedbacks => Set<CrowdSourcedFieldDefinitionFeedback>();
		public DbSet<CrowdSourcedFieldDefinitionHistoryLog> CrowdSourcedFieldDefinitionHistoryLogs => Set<CrowdSourcedFieldDefinitionHistoryLog>();
		public DbSet<CrowdSourcedFieldUserEntry> CrowdSourcedFieldUserEntries => Set<CrowdSourcedFieldUserEntry>();
		public DbSet<CrowdSourcedFieldUserEntryValue> CrowdSourcedFieldUserEntryValues => Set<CrowdSourcedFieldUserEntryValue>();
		public DbSet<CrowdSourcedFieldVerifiedEntry> CrowdSourcedFieldVerifiedEntries => Set<CrowdSourcedFieldVerifiedEntry>();
		public DbSet<CrowdSourcedFieldVerifiedEntryValue> CrowdSourcedFieldVerifiedEntryValues => Set<CrowdSourcedFieldVerifiedEntryValue>();
	}
}
