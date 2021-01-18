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
			bool mapVerifiedEntries = false)
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
			}

			return comicVM;
		}

		public ApplicationUserViewModel MapApplicationUser(ApplicationUser user)
		{
			return new ApplicationUserViewModel()
			{
				Name = user.UserName,
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

		public CrowdSourcedFieldDefinitionViewModel MapCrowdSourcedFieldDefinition(CrowdSourcedFieldDefinition def, bool mapCreatedByUser = false, bool mapLastUpdatedByUser = false)
		{
			CrowdSourcedFieldDefinitionViewModel defVM = new CrowdSourcedFieldDefinitionViewModel()
			{
				Id = def.Id,
				IsActive = def.IsActive,
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
			if (mapLastUpdatedByUser)
			{
				defVM.LastUpdatedByUser = MapApplicationUser(def.LastUpdatedByUser);
			}

			return defVM;
		}
	}
}
