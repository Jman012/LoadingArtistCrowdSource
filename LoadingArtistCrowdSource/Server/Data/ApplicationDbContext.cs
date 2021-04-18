using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.EntityFramework.Options;

using LoadingArtistCrowdSource.Server.Models;
using LoadingArtistCrowdSource.Shared.Enums;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
			// Properties
			builder.Entity<Comic>()
				.Property(c => c.Id)
				.ValueGeneratedNever();
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
				.WithMany(csfd => csfd!.ComicHistoryLogs)
				.HasForeignKey(chl => chl.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ComicHistoryLog>()
				.HasOne(chl => chl.CreatedByUser)
				.WithMany(au => au.ComicHistoryLogsCreated)
				.HasForeignKey(chl => chl.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			// Properties
			builder.Entity<ComicHistoryLog>()
				.Property(chl => chl.Id)
				.HasValueGenerator((a, b) => 
					new Services.AutoIncrementIdValueGenerator<ComicHistoryLog, int>(
						chl => chl.ComicId,
						chl => chl.Id));
			#endregion ComicHistoryLog

			#region ComicTag
			// Keys
			builder.Entity<ComicTag>()
				.HasKey(ct => new 
				{
					ct.ComicId,
					ct.Value,
				});
			// Relationships
			builder.Entity<ComicTag>()
				.HasOne(ct => ct.Comic)
				.WithMany(c => c.ComicTags)
				.HasForeignKey(ct => ct.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ComicTag>()
				.HasOne(ct => ct.CreatedByUser)
				.WithMany(au => au.ComicsTagged)
				.HasForeignKey(ct => ct.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion ComicTag

			#region ComicTranscript
			// Keys
			builder.Entity<ComicTranscript>()
				.HasKey(ct => ct.ComicId);
			// Relationships
			builder.Entity<ComicTranscript>()
				.HasOne(ct => ct.Comic)
				.WithOne(c => c.ComicTranscript!)
				.HasForeignKey<ComicTranscript>(ct => ct.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ComicTranscript>()
				.HasOne(ct => ct.LastEditedByUser)
				.WithMany(au => au.ComicTranscriptsOwned)
				.HasForeignKey(ct => ct.LastEditedBy)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion ComicTranscript

			#region ComicTranscriptHistory
			// Keys
			builder.Entity<ComicTranscriptHistory>()
				.HasKey(cth => new 
				{
					cth.ComicId,
					cth.Id,
				});
			// Relationships
			builder.Entity<ComicTranscriptHistory>()
				.HasOne(cth => cth.Comic)
				.WithMany(c => c.ComicTranscriptHistories)
				.HasForeignKey(cth => cth.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ComicTranscriptHistory>()
				.HasOne(cth => cth.CreatedByUser)
				.WithMany(au => au.ComicTranscriptHistoriesCreated)
				.HasForeignKey(cth => cth.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			// Properties
			builder.Entity<ComicTranscriptHistory>()
				.Property(ctl => ctl.Id)
				.HasValueGenerator((a, b) => 
					new Services.AutoIncrementIdValueGenerator<ComicTranscriptHistory, int>(
						cth => cth.ComicId,
						cth => cth.Id
					));
			#endregion ComicTranscriptHistory

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
				.WithMany(au => au!.CrowdSourcedFieldDefinitionsLastUpdated)
				.HasForeignKey(csfd => csfd.LastUpdatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			// Indices
			builder.Entity<CrowdSourcedFieldDefinition>()
				.HasIndex(c => c.Code)
				.IsUnique();
			// Properties
			builder.Entity<CrowdSourcedFieldDefinition>()
				.Property(csfd => csfd.Type)
				.HasConversion(new EnumToStringConverter<CrowdSourcedFieldType>());
			builder.Entity<CrowdSourcedFieldDefinition>()
				.Property(csfd => csfd.IsUsableForStatistics)
				.HasComputedColumnSql($"CAST(CASE WHEN [{nameof(CrowdSourcedFieldDefinition.IsActive)}] = 1 AND [{nameof(CrowdSourcedFieldDefinition.IsDeleted)}] <> 1 AND [{nameof(CrowdSourcedFieldDefinition.Type)}] <> '{CrowdSourcedFieldType.Section.ToString()}' THEN 1 ELSE 0 END AS BIT)", stored: false);
			#endregion CrowdSourcedFieldDefinition

			#region CrowdSourcedFieldDefinitionFeedback
			// Keys
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.HasKey(csfdf => new
				{
					csfdf.ComicId,
					csfdf.CrowdSourcedFieldDefinitionId,
					csfdf.Id,
				});
			// Relationships
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.HasOne(csfdf => csfdf.Comic)
				.WithMany(c => c.FieldFeedbacks)
				.HasForeignKey(csfdf => csfdf.ComicId)
				.OnDelete(DeleteBehavior.NoAction);
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
				.WithMany(au => au!.CrowdSourcedFieldDefinitionFeedbacksCompleted)
				.HasForeignKey(csfdf => csfdf.CompletedBy)
				.OnDelete(DeleteBehavior.NoAction);
			// Properties
			builder.Entity<CrowdSourcedFieldDefinitionFeedback>()
				.Property(csfdf => csfdf.CompletionType)
				.HasConversion(new EnumToStringConverter<CrowdSourcedFieldDefinitionFeedbackCompletion>());
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
				.HasOne(csfdhl => csfdhl.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldDefinitionHistoryLogs)
				.HasForeignKey(csfdhl => csfdhl.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<CrowdSourcedFieldDefinitionHistoryLog>()
				.HasOne(csfdhl => csfdhl.CreatedByUser)
				.WithMany(au => au.CrowdSourcedFieldDefinitionHistoryLogsCreated)
				.HasForeignKey(csfdhl => csfdhl.CreatedBy)
				.OnDelete(DeleteBehavior.NoAction);
			// Properties
			builder.Entity<CrowdSourcedFieldDefinitionHistoryLog>()
				.Property(csfdhl => csfdhl.Id)
				.HasValueGenerator((a, b) => 
					new Services.AutoIncrementIdValueGenerator<CrowdSourcedFieldDefinitionHistoryLog, Guid>(
						csfdhl => csfdhl.CrowdSourcedFieldDefinitionId, 
						csfdhl => csfdhl.Id));
			#endregion CrowdSourcedFieldDefinitionHistoryLog

			#region CrowdSourcedFieldDefinitionOption
			// Keys
			builder.Entity<CrowdSourcedFieldDefinitionOption>()
				.HasKey(csfdo => new { csfdo.CrowdSourcedFieldDefinitionId, csfdo.Code });
			// Relationships
			builder.Entity<CrowdSourcedFieldDefinitionOption>()
				.HasOne(csfdo => csfdo.CrowdSourcedFieldDefinition)
				.WithMany(csfd => csfd.CrowdSourcedFieldDefinitionOptions)
				.HasForeignKey(csfdo => csfdo.CrowdSourcedFieldDefinitionId)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion CrowdSourcedFieldDefinitionOption

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

			string user1Id = "432ea055-ea01-443d-a6f7-e97d2c18d275";
			var adminUser = new ApplicationUser()
			{
				Id = user1Id,
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "jman012guy@gmail.com",
				NormalizedEmail = "JMAN012GUY@GMAIL.COM",
				EmailConfirmed = true,
				ConcurrencyStamp = "3acb17f1-65fe-4eac-bc2b-26403b23b999",
				AccessFailedCount = 0,
				LockoutEnabled = true,
				LockoutEnd = DateTimeOffset.MinValue,
				PasswordHash = "",
				PhoneNumber = "",
				PhoneNumberConfirmed = false,
				SecurityStamp = "",
				TwoFactorEnabled = false,
			};
			adminUser.PasswordHash = "AQAAAAEAACcQAAAAEK1gJpnKWF92WxUNfQ0m0rbjpk9K5isdrfTJQzBieoSS5AJP4LQ6wxDHGwor1uT86A=="; // new Microsoft.AspNetCore.Identity.PasswordHasher<ApplicationUser>().HashPassword(adminUser, "password");
			builder.Entity<ApplicationUser>().HasData(
				adminUser
			);
		}

		public DbSet<Comic> Comics => Set<Comic>();
		public DbSet<ComicHistoryLog> ComicHistoryLogs => Set<ComicHistoryLog>();
		public DbSet<ComicTag> ComicTags => Set<ComicTag>();
		public DbSet<ComicTranscript> ComicTranscripts => Set<ComicTranscript>();
		public DbSet<ComicTranscriptHistory> ComicTranscriptHistories => Set<ComicTranscriptHistory>();
		public DbSet<CrowdSourcedFieldDefinition> CrowdSourcedFieldDefinitions => Set<CrowdSourcedFieldDefinition>();
		public DbSet<CrowdSourcedFieldDefinitionFeedback> CrowdSourcedFieldDefinitionFeedbacks => Set<CrowdSourcedFieldDefinitionFeedback>();
		public DbSet<CrowdSourcedFieldDefinitionHistoryLog> CrowdSourcedFieldDefinitionHistoryLogs => Set<CrowdSourcedFieldDefinitionHistoryLog>();
		public DbSet<CrowdSourcedFieldUserEntry> CrowdSourcedFieldUserEntries => Set<CrowdSourcedFieldUserEntry>();
		public DbSet<CrowdSourcedFieldUserEntryValue> CrowdSourcedFieldUserEntryValues => Set<CrowdSourcedFieldUserEntryValue>();
		public DbSet<CrowdSourcedFieldVerifiedEntry> CrowdSourcedFieldVerifiedEntries => Set<CrowdSourcedFieldVerifiedEntry>();
		public DbSet<CrowdSourcedFieldVerifiedEntryValue> CrowdSourcedFieldVerifiedEntryValues => Set<CrowdSourcedFieldVerifiedEntryValue>();
	}
}
