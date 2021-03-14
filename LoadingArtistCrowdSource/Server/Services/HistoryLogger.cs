using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Server.Models;
using LoadingArtistCrowdSource.Shared.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class HistoryLogger
	{
		private readonly ApplicationDbContext _context;

		public HistoryLogger(ApplicationDbContext context)
		{
			_context = context;
		}

		#region ComicHistoryLog
		public ComicHistoryLog CreateComicImportedLog(Comic comic)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = null,
				CreatedBy = comic.ImportedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = "Comic imported into system",
				OldValue = null,
				NewValue = null,
			};
		}

		public ComicHistoryLog CreateAddUserEntryLog(Comic comic, CrowdSourcedFieldUserEntry userEntry, string[] newValues)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = userEntry.CrowdSourcedFieldDefinitionId,
				CreatedBy = userEntry.CreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = "User added new field entry",
				OldValue = RenderValues(userEntry),
				NewValue = RenderValues(newValues),
			};
		}

		public ComicHistoryLog CreateEditUserEntryLog(Comic comic, CrowdSourcedFieldUserEntry userEntry, string[] newValues)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = userEntry.CrowdSourcedFieldDefinitionId,
				CreatedBy = userEntry.CreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = "User edited existing field entry",
				OldValue = RenderValues(userEntry),
				NewValue = RenderValues(newValues),
			};
		}

		public ComicHistoryLog CreateAddVerifiedEntryLog(Comic comic, CrowdSourcedFieldVerifiedEntry verifiedEntry)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = verifiedEntry.CrowdSourcedFieldDefinitionId,
				CreatedBy = verifiedEntry.FirstCreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = "Field has gained consensus, and verified",
				OldValue = RenderNoValues(),
				NewValue = RenderValues(verifiedEntry),
			};
		}

		public ComicHistoryLog CreateRemoveVerifiedEntryLog(Comic comic, CrowdSourcedFieldVerifiedEntry verifiedEntry)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = verifiedEntry.CrowdSourcedFieldDefinitionId,
				CreatedBy = verifiedEntry.FirstCreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = "Field value has been contested, and unverified",
				OldValue = RenderValues(verifiedEntry),
				NewValue = RenderNoValues(),
			};
		}

		public IEnumerable<ComicHistoryLog> CreateMetadataEditedLogs(Comic oldComic, Shared.Models.ComicViewModel newValuesVm, string byUserId)
		{
			Func<ComicHistoryLog> create = () => new ComicHistoryLog()
			{
				ComicId = oldComic.Id,
				CrowdSourcedFieldDefinitionId = null,
				CreatedBy = byUserId,
				LogDate = DateTimeOffset.Now,
			};

			Func<string?, string?, string, ComicHistoryLog?> createWith = (oldValue, newValue, field) =>
			{
				if (oldValue != newValue) return null;
				var log = create();
				log.LogMessage = $"Comic metadata field '{field}' was edited";
				log.OldValue = oldValue;
				log.NewValue = newValue;
				return log;	
			};

			return new ComicHistoryLog?[]
			{
				createWith(oldComic.Code, newValuesVm.Code, Utilities.GetDisplayName(() => newValuesVm.Code)),
				createWith(oldComic.Permalink, newValuesVm.Permalink, Utilities.GetDisplayName(() => newValuesVm.Permalink)),
				createWith(oldComic.Title, newValuesVm.Title, Utilities.GetDisplayName(() => newValuesVm.Title)),
				createWith(oldComic.Tooltip, newValuesVm.Tooltip, Utilities.GetDisplayName(() => newValuesVm.Tooltip)),
				createWith(oldComic.Description, newValuesVm.Description, Utilities.GetDisplayName(() => newValuesVm.Description)),
				createWith(oldComic.ImageUrlSrc, newValuesVm.ImageUrlSrc, Utilities.GetDisplayName(() => newValuesVm.ImageUrlSrc)),
				createWith(oldComic.ImageThumbnailUrlSrc, newValuesVm.ImageThumbnailUrlSrc, Utilities.GetDisplayName(() => newValuesVm.ImageThumbnailUrlSrc)),
				createWith(oldComic.ImageWideThumbnailUrlSrc, newValuesVm.ImageWideThumbnailUrlSrc, Utilities.GetDisplayName(() => newValuesVm.ImageWideThumbnailUrlSrc)),
			}
				.Where(chl => chl != null)
				.Select(chl => chl!);
		}

		public ComicHistoryLog CreateNewTranscriptLog(Comic comic, ComicTranscriptHistory? previousTranscriptHistory, ComicTranscriptHistory newTranscriptHistory)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = null,
				CreatedBy = newTranscriptHistory.CreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = "User submitted comic transcript",
				OldValue = previousTranscriptHistory?.TranscriptContent,
				NewValue = newTranscriptHistory.TranscriptContent,
			};
		}
		
		public ComicHistoryLog CreateTranscriptRollbackLog(Comic comic, ComicTranscriptHistory previousTranscriptHistory, ComicTranscriptHistory newTranscriptHistory)
		{
			return new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = null,
				CreatedBy = newTranscriptHistory.CreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = $"User rolled back to previous transcript, submitted by {previousTranscriptHistory.CreatedByUser.UserName} on {previousTranscriptHistory.CreatedDate}",
				OldValue = previousTranscriptHistory.TranscriptContent,
				NewValue = newTranscriptHistory.TranscriptContent,
			};
		}
		#endregion ComicHistoryLog

		#region CrowdSourcedFieldDefinitionHistoryLog
		public CrowdSourcedFieldDefinitionHistoryLog CreateAddFieldDefinitionLog(CrowdSourcedFieldDefinition fieldDef)
		{
			return new CrowdSourcedFieldDefinitionHistoryLog()
			{
				CrowdSourcedFieldDefinitionId = fieldDef.Id,
				CreatedBy = fieldDef.CreatedBy,
				LogDate = DateTimeOffset.Now,
				LogMessage = $"User created new field",
				OldValue = null,
				NewValue = null,
			};
		}

		public IEnumerable<CrowdSourcedFieldDefinitionHistoryLog> CreateEditFieldDefinitionLogs(
			CrowdSourcedFieldDefinition oldFieldDef, 
			Shared.Models.FieldDefinitionFormViewModel newFieldVm, 
			string byUserId)
		{
			Func<string?, string?, string, CrowdSourcedFieldDefinitionHistoryLog?> createWith = (oldValue, newValue, field) =>
			{
				if (oldValue == newValue) return null;
				var log = CreateBlankFieldDefinitionLog(oldFieldDef, byUserId);
				log.LogMessage = $"Field definition property '{field}' was edited";
				log.OldValue = oldValue;
				log.NewValue = newValue;
				return log;	
			};

			return new CrowdSourcedFieldDefinitionHistoryLog?[]
			{
				createWith(oldFieldDef.Code, newFieldVm.Code, Utilities.GetDisplayName(() => newFieldVm.Code)),
				createWith(oldFieldDef.IsActive.ToString(), newFieldVm.IsActive.ToString(), Utilities.GetDisplayName(() => newFieldVm.IsActive)),
				createWith(Utilities.GetEnumDescription(oldFieldDef.Type), Utilities.GetEnumDescription(newFieldVm.Type), Utilities.GetDisplayName(() => newFieldVm.Type)),
				createWith(oldFieldDef.Name, newFieldVm.Name, Utilities.GetDisplayName(() => newFieldVm.Name)),
				createWith(oldFieldDef.ShortDescription, newFieldVm.ShortDescription, Utilities.GetDisplayName(() => newFieldVm.ShortDescription)),
				createWith(oldFieldDef.LongDescription, newFieldVm.LongDescription, Utilities.GetDisplayName(() => newFieldVm.LongDescription)),
			}
				.Concat(CreateEditFieldDefinitionOptionLogs(oldFieldDef, newFieldVm, byUserId))
				.Where(chl => chl != null)
				.Select(chl => chl!);
		}

		private IEnumerable<CrowdSourcedFieldDefinitionHistoryLog> CreateEditFieldDefinitionOptionLogs(
			CrowdSourcedFieldDefinition oldFieldDef, 
			Shared.Models.FieldDefinitionFormViewModel newFieldVm, 
			string byUserId)
		{
			var hshOldOptionCodes = new HashSet<string>(oldFieldDef.CrowdSourcedFieldDefinitionOptions.Select(csfdo => csfdo.Code));
			var hshNewOptionCodes = new HashSet<string>(newFieldVm.Options.Select(o => o.Code));
			var dctOldOptions = oldFieldDef.CrowdSourcedFieldDefinitionOptions.ToDictionary(csfdo => csfdo.Code);
			var dctNewOptions = newFieldVm.Options.ToDictionary(o => o.Code);

			var addedOptionCodes = hshNewOptionCodes.Except(hshOldOptionCodes);
			var deletedOptionCodes = hshOldOptionCodes.Except(hshNewOptionCodes);
			var maybeEditedOptionCodes = hshOldOptionCodes.Intersect(hshNewOptionCodes);

			foreach (var code in addedOptionCodes)
			{
				var log = CreateBlankFieldDefinitionLog(oldFieldDef, byUserId);
				log.LogMessage = $"Field definition option '{code}' was added";
				log.OldValue = null;
				log.NewValue = null;
				yield return log;
			}

			foreach (var code in deletedOptionCodes)
			{
				var log = CreateBlankFieldDefinitionLog(oldFieldDef, byUserId);
				log.LogMessage = $"Field definition option '{code}' was deleted";
				log.OldValue = null;
				log.NewValue = null;
				yield return log;
			}

			foreach (var code in maybeEditedOptionCodes)
			{
				Func<string?, string?, string, CrowdSourcedFieldDefinitionHistoryLog?> createWith = (oldValue, newValue, field) => 
				{
					if (oldValue == newValue) return null;
					var log = CreateBlankFieldDefinitionLog(oldFieldDef, byUserId);
					log.LogMessage = $"Field definition option '{code}' was edited for field {field}";
					log.OldValue = oldValue;
					log.NewValue = newValue;
					return log;
				};

				CrowdSourcedFieldDefinitionOption oldOption = dctOldOptions[code];
				Shared.Models.CrowdSourcedFieldDefinitionOptionViewModel newOption = dctNewOptions[code];
				var fields = new CrowdSourcedFieldDefinitionHistoryLog?[] {
					createWith(oldOption.Text, newOption.Text, Utilities.GetDisplayName(() => newOption.Text)),
					createWith(oldOption.Description, newOption.Description, Utilities.GetDisplayName(() => newOption.Description)),
					createWith(oldOption.URL, newOption.URL, Utilities.GetDisplayName(() => newOption.URL)),
				}
					.Where(csfdhl => csfdhl != null)
					.Select(csfdhl => csfdhl!);
				foreach (var csfdhl in fields)
				{
					yield return csfdhl;
				}
			}
		}

		private CrowdSourcedFieldDefinitionHistoryLog CreateBlankFieldDefinitionLog(
			CrowdSourcedFieldDefinition oldFieldDef, 
			string byUserId)
		{
			return new CrowdSourcedFieldDefinitionHistoryLog()
			{
				CrowdSourcedFieldDefinitionId = oldFieldDef.Id,
				CreatedBy = byUserId,
				LogDate = DateTimeOffset.Now,
			};
		}
		#endregion CrowdSourcedFieldDefinitionHistoryLog

		#region Private Methods
		private string RenderValues(CrowdSourcedFieldUserEntry userEntry)
		{
			return RenderValues(userEntry.CrowdSourcedFieldUserEntryValues.Select(v => v.Value));
		}

		private string RenderValues(CrowdSourcedFieldVerifiedEntry verifiedEntry)
		{
			return RenderValues(verifiedEntry.CrowdSourcedFieldVerifiedEntryValues.Select(v => v.Value));
		}

		private string RenderValues(IEnumerable<string> values)
		{
			if (values == null || !values.Any())
			{
				return RenderNoValues();
			}

			return string.Join(", ", values);
		}

		private string RenderNoValues()
		{
			return "<empty>";
		}
		#endregion Private Methods
	}
}
