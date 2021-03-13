using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Server.Models;

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

		public ComicHistoryLog CreateAddUserEntryLog(Comic comic, CrowdSourcedFieldUserEntry userEntry, string[] newValues)
		{
			var historyLog = new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = userEntry.CrowdSourcedFieldDefinitionId,
				CreatedBy = userEntry.CreatedBy,
				LogDate = DateTimeOffset.Now,

			};
			return historyLog;
		}

		public ComicHistoryLog CreateEditUserEntryLog(Comic comic, CrowdSourcedFieldUserEntry userEntry, string[] newValues)
		{
			var historyLog = new ComicHistoryLog()
			{
				ComicId = comic.Id,
				CrowdSourcedFieldDefinitionId = userEntry.CrowdSourcedFieldDefinitionId,
				CreatedBy = userEntry.CreatedBy,
				LogDate = DateTimeOffset.Now,

			};
			return historyLog;
		}
	}
}
