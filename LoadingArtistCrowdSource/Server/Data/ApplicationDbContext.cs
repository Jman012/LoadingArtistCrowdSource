using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.EntityFramework.Options;

using LoadingArtistCrowdSource.Server.Models;
using LoadingArtistCrowdSource.Shared.Enums;

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
			// Indices
			builder.Entity<Comic>()
				.HasIndex(c => c.Code)
				.IsUnique();
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

			string user1Id = "432ea055-ea01-443d-a6f7-e97d2c18d275", user2Id = "432ea055-ea01-443d-a6f7-e97d2c18d276", user3Id = "432ea055-ea01-443d-a6f7-e97d2c18d277";
			var adminUser = new ApplicationUser()
			{
				Id = user1Id,
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
			adminUser.PasswordHash = new Microsoft.AspNetCore.Identity.PasswordHasher<ApplicationUser>().HashPassword(adminUser, "password");
			builder.Entity<ApplicationUser>().HasData(
				adminUser,
				new ApplicationUser()
				{
					Id = user2Id,
					UserName = "jamamp1",
					NormalizedUserName = "JAMAMP1",
					Email = "jman012guy+1@gmail.com",
					NormalizedEmail = "JMAN012GUY+1@GMAIL.COM",
					EmailConfirmed = true,
					ConcurrencyStamp = "3acb17f1-65fe-4eac-bc2b-26403b23b998",
					AccessFailedCount = 0,
					LockoutEnabled = true,
					LockoutEnd = null,
					PasswordHash = "",
					PhoneNumber = "",
					PhoneNumberConfirmed = false,
					SecurityStamp = "",
					TwoFactorEnabled = false,
				},
				new ApplicationUser()
				{
					Id = user3Id,
					UserName = "jamamp2",
					NormalizedUserName = "JAMAMP2",
					Email = "jman012guy+2@gmail.com",
					NormalizedEmail = "JMAN012GUY+2@GMAIL.COM",
					EmailConfirmed = true,
					ConcurrencyStamp = "3acb17f1-65fe-4eac-bc2b-26403b23b997",
					AccessFailedCount = 0,
					LockoutEnabled = true,
					LockoutEnd = null,
					PasswordHash = "",
					PhoneNumber = "",
					PhoneNumberConfirmed = false,
					SecurityStamp = "",
					TwoFactorEnabled = false,
				}
			);
			builder.Entity<Comic>().HasData(new Comic()
			{
				Id = 1,
				Code = "born",
				Permalink = "https://loadingartist.com/comic/born/",
				ComicPublishedDate = DateTimeOffset.Parse("2011-01-04T00:00:00.000-0800"),
				Title = "Born",
				Tooltip = "Born",
				Description = null,
				ImageUrlSrc = "https://loadingartist.com/wp-content/uploads/2011/07/2011-01-04-born.png",
				ImageThumbnailUrlSrc = "https://loadingartist.com/comic-thumbs/born.png",
				ImageWideThumbnailUrlSrc = null,
				ImportedDate = DateTimeOffset.Now,
				ImportedBy = "432ea055-ea01-443d-a6f7-e97d2c18d275",
				LastUpdatedByUser = null,
				LastUpdatedBy = null,
			});
			Guid def1Id = Guid.NewGuid(), def2Id = Guid.NewGuid();
			builder.Entity<CrowdSourcedFieldDefinition>().HasData(new CrowdSourcedFieldDefinition()
			{
				Id = def1Id,
				IsActive = true,
				IsDeleted = false,
				Type = CrowdSourcedFieldType.Number,
				DisplayOrder = 1,
				Name = "Panels",
				ShortDescription = "The number of panels in the comic",
				LongDescription = "blah blah",
				CreatedDate = DateTimeOffset.Now,
				CreatedBy = user1Id,
				LastUpdatedDate = null,
				LastUpdatedBy = null,
			});
			builder.Entity<CrowdSourcedFieldDefinition>().HasData(new CrowdSourcedFieldDefinition()
			{
				Id = def2Id,
				IsActive = true,
				IsDeleted = false,
				Type = CrowdSourcedFieldType.FreeformText,
				DisplayOrder = 2,
				Name = "Characters",
				ShortDescription = "Which characters are present in the comic",
				LongDescription = "blah blah blah blah",
				CreatedDate = DateTimeOffset.Now,
				CreatedBy = user1Id,
				LastUpdatedDate = null,
				LastUpdatedBy = null,
			});
			builder.Entity<CrowdSourcedFieldUserEntry>().HasData(
				new CrowdSourcedFieldUserEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					CreatedBy = user1Id,
					CreatedDate = DateTimeOffset.Now,
				},
				new CrowdSourcedFieldUserEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					CreatedBy = user2Id,
					CreatedDate = DateTimeOffset.Now,
				},
				new CrowdSourcedFieldUserEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					CreatedBy = user3Id,
					CreatedDate = DateTimeOffset.Now,
				},
				new CrowdSourcedFieldUserEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user1Id,
					CreatedDate = DateTimeOffset.Now,
				},
				new CrowdSourcedFieldUserEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user2Id,
					CreatedDate = DateTimeOffset.Now,
				},
				new CrowdSourcedFieldUserEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user3Id,
					CreatedDate = DateTimeOffset.Now,
				}
			);
			builder.Entity<CrowdSourcedFieldUserEntryValue>().HasData(
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					CreatedBy = user1Id,
					Id = 0,
					Value = "3",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					CreatedBy = user2Id,
					Id = 0,
					Value = "3",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					CreatedBy = user3Id,
					Id = 0,
					Value = "3",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user1Id,
					Id = 0,
					Value = "Hat Guy",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user2Id,
					Id = 0,
					Value = "Hat Guy",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user3Id,
					Id = 0,
					Value = "Hat Guy",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user1Id,
					Id = 1,
					Value = "Jes",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user2Id,
					Id = 1,
					Value = "Jes",
				},
				new CrowdSourcedFieldUserEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					CreatedBy = user3Id,
					Id = 1,
					Value = "Jes",
				}
			);
			builder.Entity<CrowdSourcedFieldVerifiedEntry>().HasData(
				new CrowdSourcedFieldVerifiedEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					FirstCreatedBy = user1Id,
					VerificationDate = DateTimeOffset.Now,
				},
				new CrowdSourcedFieldVerifiedEntry()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					FirstCreatedBy = user1Id,
					VerificationDate = DateTimeOffset.Now,
				});
			builder.Entity<CrowdSourcedFieldVerifiedEntryValue>().HasData(
				new CrowdSourcedFieldVerifiedEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def1Id,
					Id = 0,
					Value = "3",
				},
				new CrowdSourcedFieldVerifiedEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					Id = 0,
					Value = "Blue",
				},
				new CrowdSourcedFieldVerifiedEntryValue()
				{
					ComicId = 1,
					CrowdSourcedFieldDefinitionId = def2Id,
					Id = 1,
					Value = "Hat Guy",
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
