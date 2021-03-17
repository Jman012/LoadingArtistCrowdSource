using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Models;
using LoadingArtistCrowdSource.Shared.Models;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class ModelMapper
	{
		public ComicViewModel MapComic(Comic comic,
			bool mapImportedByUser = false,
			bool mapLastUpdatedUser = false,
			bool mapTranscript = false,
			bool mapTags = false)
		{
			ComicViewModel comicVM = new ComicViewModel()
			{
				Id = comic.Id,
				Code = comic.Code,
				Permalink = comic.Permalink,
				ComicPublishedDate = comic.ComicPublishedDate,
				Title = comic.Title,
				Tooltip = comic.Tooltip,
				Description = comic.Description,
				ImageUrlSrc = comic.ImageUrlSrc,
				ImageThumbnailUrlSrc = comic.ImageThumbnailUrlSrc,
				ImageWideThumbnailUrlSrc = comic.ImageWideThumbnailUrlSrc,
				ImportedDate = comic.ImportedDate,
				LastUpdatedDate = comic.LastUpdatedDate,
			};

			if (mapImportedByUser)
			{
				comicVM.ImportedByUser = MapApplicationUser(comic.ImportedByUser);
			}
			if (mapLastUpdatedUser && comic.LastUpdatedByUser != null)
			{
				comicVM.LastUpdatedByUser = MapApplicationUser(comic.LastUpdatedByUser);
			}
			if (mapTranscript && comic.ComicTranscript != null)
			{
				comicVM.Transcript = MapTranscript(comic.ComicTranscript, mapLastEditedByUser: true);
			}
			if (mapTags && comic.ComicTags != null)
			{
				comicVM.Tags = MapComicTags(comic.ComicTags, mapApplicationUser: true);
			}

			return comicVM;
		}

		public ComicListItemViewModel MapComicListItem(Comic comic)
		{
			ComicListItemViewModel comicVM = new ComicListItemViewModel()
			{
				Id = comic.Id,
				Code = comic.Code,
				ComicPublishedDate = comic.ComicPublishedDate,
				Title = comic.Title,
				ImageThumbnailUrlSrc = comic.ImageThumbnailUrlSrc,
			};

			return comicVM;
		}

		public ApplicationUserViewModel MapApplicationUser(ApplicationUser user)
		{
			return new ApplicationUserViewModel()
			{
				UserName = user.UserName,
			};
		}

		public CrowdSourcedFieldUserEntryViewModel MapCrowdSourcedFieldUserEntry(CrowdSourcedFieldUserEntry entry, bool mapCreatedBy = false)
		{
			var vm = new CrowdSourcedFieldUserEntryViewModel()
			{
				CreatedDate = entry.CreatedDate,
				Values = entry.CrowdSourcedFieldUserEntryValues.Select(v => v.Value).ToList(),
			};

			if (mapCreatedBy)
			{
				vm.CreatedByUser = MapApplicationUser(entry.CreatedByUser);
			}

			return vm;
		}

		public CrowdSourcedFieldVerifiedEntryViewModel MapCrowdSourcedFieldVerifiedEntry(CrowdSourcedFieldVerifiedEntry entry, bool mapFirstCreatedByUser = false, bool mapFieldDefinition = false)
		{
			CrowdSourcedFieldVerifiedEntryViewModel verifEntry = new CrowdSourcedFieldVerifiedEntryViewModel()
			{
				VerificationDate = entry.VerificationDate,
				Values = entry.CrowdSourcedFieldVerifiedEntryValues.Select(ev => ev.Value).ToList(),
			};

			if (mapFirstCreatedByUser)
			{
				verifEntry.FirstCreatedByUser = MapApplicationUser(entry.FirstCreatedByUser);
			}
			if (mapFieldDefinition)
			{
				verifEntry.CrowdSourcedFieldDefinition = MapCrowdSourcedFieldDefinition(entry.CrowdSourcedFieldDefinition);
			}

			return verifEntry;
		}

		public CrowdSourcedFieldDefinitionViewModel MapCrowdSourcedFieldDefinition(CrowdSourcedFieldDefinition def, bool mapCreatedByUser = false, bool mapLastUpdatedByUser = false, bool mapOptions = false)
		{
			CrowdSourcedFieldDefinitionViewModel defVM = new CrowdSourcedFieldDefinitionViewModel()
			{
				Id = def.Id,
				Code = def.Code,
				IsActive = def.IsActive,
				IsDeleted = def.IsDeleted,
				Type = def.Type,
				DisplayOrder = def.DisplayOrder,
				Name = def.Name,
				ShortDescription = def.ShortDescription,
				LongDescription = def.LongDescription,
				CreatedDate = def.CreatedDate,
				LastUpdatedDate = def.LastUpdatedDate,
			};

			if (mapCreatedByUser)
			{
				defVM.CreatedByUser = MapApplicationUser(def.CreatedByUser);
			}
			if (mapLastUpdatedByUser && def.LastUpdatedByUser != null)
			{
				defVM.LastUpdatedByUser = MapApplicationUser(def.LastUpdatedByUser);
			}
			if (mapOptions)
			{
				defVM.Options = def.CrowdSourcedFieldDefinitionOptions.Select(this.MapCrowdSourcedFieldDefinitionOption).ToList();
			}

			return defVM;
		}

		public CrowdSourcedFieldDefinitionOptionViewModel MapCrowdSourcedFieldDefinitionOption(CrowdSourcedFieldDefinitionOption option)
		{
			CrowdSourcedFieldDefinitionOptionViewModel optionVM = new CrowdSourcedFieldDefinitionOptionViewModel()
			{
				Code = option.Code,
				Text = option.Text,
				Description = option.Description,
				URL = option.URL,
			};

			return optionVM;
		}

		public FieldDefinitionFormViewModel MapFieldDefinitionForm(CrowdSourcedFieldDefinition def, bool mapOptions = false)
		{
			FieldDefinitionFormViewModel vm = new FieldDefinitionFormViewModel()
			{
				Code = def.Code,
				Name = def.Name,
				IsActive = def.IsActive,
				Type = def.Type,
				ShortDescription = def.ShortDescription,
				LongDescription = def.LongDescription,
				CreatedDate = def.CreatedDate,
				CreatedBy = def.CreatedByUser.UserName,
				LastUpdatedDate = def.LastUpdatedDate,
				LastUpdatedBy = def.LastUpdatedByUser?.UserName,
			};

			if (mapOptions)
			{
				vm.Options = def.CrowdSourcedFieldDefinitionOptions.Select(this.MapCrowdSourcedFieldDefinitionOption).ToList();
			}

			return vm;
		}

		public FeedbackViewModel MapFeedback(CrowdSourcedFieldDefinitionFeedback feedback, bool mapCreatedBy, bool mapCompletedBy)
		{
			FeedbackViewModel vm = new FeedbackViewModel()
			{
				ComicCode = feedback.Comic.Code,
				FieldCode = feedback.CrowdSourcedFieldDefinition.Code,
				Id = feedback.Id,
				CreatedDate = feedback.CreatedDate,
				Comment = feedback.Comment,
				CompletionDate = feedback.CompletionDate,
				CompletionType = feedback.CompletionType,
				CompletionComment = feedback.CompletionComment,
			};

			if (mapCreatedBy)
			{
				vm.CreatedByUser = MapApplicationUser(feedback.CreatedByUser);
			}
			if (mapCompletedBy && feedback.CompletedByUser != null)
			{
				vm.CompletedByUser = MapApplicationUser(feedback.CompletedByUser);
			}

			return vm;
		}

		public TranscriptViewModel MapTranscript(Models.ComicTranscript transcript,
			bool mapLastEditedByUser = false)
		{
			var vm = new TranscriptViewModel()
			{
				LastEditedDate = transcript.LastEditedDate,
				TranscriptContent = transcript.TranscriptContent,
			};

			if (mapLastEditedByUser)
			{
				vm.LastEditedByUser = MapApplicationUser(transcript.LastEditedByUser);
			}

			return vm;
		}

		public TranscriptHistoryItemViewModel MapTranscriptHistory(
			ComicTranscriptHistory transcriptHistory,
			bool mapCreatedByUser = false)
		{
			var vm = new TranscriptHistoryItemViewModel()
			{
				Id = transcriptHistory.Id,
				CreatedDate = transcriptHistory.CreatedDate,
				TranscriptContent = transcriptHistory.TranscriptContent,
				DiffWithPrevious = transcriptHistory.DiffWithPrevious,
			};

			if (mapCreatedByUser)
			{
				vm.CreatedByUser = MapApplicationUser(transcriptHistory.CreatedByUser);
			}

			return vm;
		}

		public ComicHistoryLogViewModel MapComicHistoryLog(
			Comic comic,
			IEnumerable<ComicHistoryLog> comicLogs)
		{
			IEnumerable<ComicHistoryLogItemViewModel> logItemVms = comicLogs
				.Select(cl => new ComicHistoryLogItemViewModel()
				{
					Id = cl.Id,
					FieldName = cl.CrowdSourcedFieldDefinition?.Name,
					CreatedByUser = MapApplicationUser(cl.CreatedByUser),
					LogDate = cl.LogDate,
					LogMessage = cl.LogMessage,
					OldValue = cl.OldValue,
					NewValue = cl.NewValue,
				});

			var vm = new ComicHistoryLogViewModel()
			{
				ComicTitle = comic.Title,
				LogItems = logItemVms.ToArray(),
			};
			return vm;
		}

		public CrowdSourcedFieldDefinitionHistoryLogViewModel MapFieldHistoryLog(
			CrowdSourcedFieldDefinition fieldDef,
			IEnumerable<CrowdSourcedFieldDefinitionHistoryLog> fieldLogs)
		{
			IEnumerable<CrowdSourcedFieldDefinitionHistoryLogItemViewModel> logItemVms = fieldLogs
				.Select(csfdhl => new CrowdSourcedFieldDefinitionHistoryLogItemViewModel()
				{
					Id = csfdhl.Id,
					CreatedByUser = MapApplicationUser(csfdhl.CreatedByUser),
					LogDate = csfdhl.LogDate,
					LogMessage = csfdhl.LogMessage,
					OldValue = csfdhl.OldValue,
					NewValue = csfdhl.NewValue,
				});

			var vm = new CrowdSourcedFieldDefinitionHistoryLogViewModel()
			{
				FieldTitle = fieldDef.Name,
				LogItems = logItemVms.ToArray(),
			};
			return vm;
		}

		public ComicTagsViewModel MapComicTags(
			List<ComicTag> comicTags,
			bool mapApplicationUser = false)
		{
			var vm = new ComicTagsViewModel()
			{
				TagValues = comicTags
					.Select(ct => new ComicTagViewModel() { TagValue = ct.Value })
					.OrderBy(v => v.TagValue, StringComparer.OrdinalIgnoreCase)
					.ToList(),
			};

			if (mapApplicationUser)
			{
				vm.Contributors = comicTags
					.Select(ct => MapApplicationUser(ct.CreatedByUser))
					.OrderBy(au => au.UserName, StringComparer.OrdinalIgnoreCase)
					.ToList();
			}

			return vm;
		}
	}
}
