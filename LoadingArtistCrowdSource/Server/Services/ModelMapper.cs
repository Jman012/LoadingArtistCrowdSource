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
			bool mapComicHistoryLogs = false, 
			bool mapUserEntries = false, 
			bool mapVerifiedEntries = false,
			bool mapVerifiedUserEntries = false)
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
			if (mapComicHistoryLogs)
			{
				comicVM.ComicHistoryLogs = comic
					.ComicHistoryLogs
					.Select(this.MapComicHistoryLog)
					.ToList();
			}
			if (mapUserEntries)
			{
				comicVM.CrowdSourcedFieldUserEntries = comic
					.CrowdSourcedFieldUserEntries
					.Select(this.MapCrowdSourcedFieldUserEntry)
					.ToList();
			}
			if (mapVerifiedEntries)
			{
				comicVM.CrowdSourcedFieldVerifiedEntries = comic
					.CrowdSourcedFieldVerifiedEntries
					.Select(ve => this.MapCrowdSourcedFieldVerifiedEntry(ve, mapFieldDefinition: true))
					.ToList();

				if (mapVerifiedUserEntries)
				{
					var userEntriesLookup = comic.CrowdSourcedFieldUserEntries.ToLookup(ue => ue.CrowdSourcedFieldDefinitionId);
					foreach (var ve in comicVM.CrowdSourcedFieldVerifiedEntries)
					{
						ve.CrowdSourcedUserEntries = userEntriesLookup[ve.CrowdSourcedFieldDefinition.Id]
							.Select(ue => new CrowdSourcedFieldUserEntryViewModel()
							{
								CreatedDate = ue.CreatedDate,
								Values = ue.CrowdSourcedFieldUserEntryValues.Select(v => v.Value).ToList(),
								CreatedByUser = this.MapApplicationUser(ue.CreatedByUser),
							})
							.ToList();
					}
				}
			}

			return comicVM;
		}

		public ApplicationUserViewModel MapApplicationUser(ApplicationUser user)
		{
			return new ApplicationUserViewModel()
			{
				UserName = user.UserName,
			};
		}

		public ComicHistoryLogViewModel MapComicHistoryLog(ComicHistoryLog log)
		{
			return new ComicHistoryLogViewModel();
		}

		public CrowdSourcedFieldUserEntryViewModel MapCrowdSourcedFieldUserEntry(CrowdSourcedFieldUserEntry entry)
		{
			return new CrowdSourcedFieldUserEntryViewModel();
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
	}
}
