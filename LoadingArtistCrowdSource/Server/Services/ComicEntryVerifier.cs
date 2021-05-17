using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoadingArtistCrowdSource.Shared.Enums;
using LoadingArtistCrowdSource.Server.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class ComicEntryVerifier
	{
		private readonly ApplicationDbContext _context;
		private readonly HistoryLogger _historyLogger;

		public ComicEntryVerifier(ApplicationDbContext context, HistoryLogger historyLogger)
		{
			_context = context;
			_historyLogger = historyLogger;
		}

		public async Task<bool?> Verify(Models.Comic comic, Models.CrowdSourcedFieldDefinition fieldDefinition)
		{
			bool? verification = null;

			// Retrieve all user entries for this comic's field.
			List<Models.CrowdSourcedFieldUserEntry> userEntriesForField = await _context.CrowdSourcedFieldUserEntries
				.Include(csfue => csfue.CrowdSourcedFieldUserEntryValues)
				.Where(csfue => csfue.ComicId == comic.Id && csfue.CrowdSourcedFieldDefinitionId == fieldDefinition.Id)
				.ToListAsync();
			// Entry Values => Entry lookup
			ILookup<string[], Models.CrowdSourcedFieldUserEntry> lkpUserEntriesForField = userEntriesForField
				.ToLookup(csfue => csfue.CrowdSourcedFieldUserEntryValues.Select(csfuev => csfuev.Value).ToArray(), // The Key of the lookup is the series of values.
					new Shared.Utilities.ArrayEqualityComparer<string>()); // Use a proper comparer for arrays of strings.
																		   // Order the above lookup by number of entries for each overall value
			List<IGrouping<string[], Models.CrowdSourcedFieldUserEntry>> userEntriesForFieldRanked = lkpUserEntriesForField
				.OrderByDescending(lkpCsfue => lkpCsfue.Count())
				.ToList();
			// Pick the top and second to top entry value sets, which will
			// be compared against below.
			IGrouping<string[], Models.CrowdSourcedFieldUserEntry>? topUserEntryValues = userEntriesForFieldRanked.FirstOrDefault();
			if (topUserEntryValues == null)
			{
				// No entries to verify. Return early.
				return verification;
			}
			IGrouping<string[], Models.CrowdSourcedFieldUserEntry>? secondUserEntryValues = userEntriesForFieldRanked.Skip(1).FirstOrDefault();

			/*
			 * The criteria for one set of values to be verified is as follows.
			 * 
			 * If the difference in number of entries for one value set and the next 
			 * (or if the next does not exist, then assume this number is 0) exceeds the set threshold, 
			 * then the top number of entries becomes verified.
			 */
			int threshold = 3;
			int numEntriesTopValueSet = lkpUserEntriesForField[topUserEntryValues.Key].Count();
			int numEntriesNextValueSet = secondUserEntryValues != null ? lkpUserEntriesForField[secondUserEntryValues.Key].Count() : 0;
			bool shouldVerify = (numEntriesTopValueSet - numEntriesNextValueSet) >= threshold;

			var verifiedEntry = await _context.CrowdSourcedFieldVerifiedEntries
					.Include(csfve => csfve.CrowdSourcedFieldVerifiedEntryValues)
					.FirstOrDefaultAsync(csfve => csfve.ComicId == comic.Id &&
										 csfve.CrowdSourcedFieldDefinitionId == fieldDefinition.Id);
			if (shouldVerify)
			{
				// Create a new VerifiedEntry record, if one doesn't already exist.
				if (verifiedEntry == null)
				{
					verification = true;

					var firstUserEntry = lkpUserEntriesForField[topUserEntryValues.Key].OrderBy(csfue => csfue.CreatedDate).First();
					verifiedEntry = new Models.CrowdSourcedFieldVerifiedEntry()
					{
						ComicId = comic.Id,
						CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
						FirstCreatedBy = firstUserEntry.CreatedBy,
						VerificationDate = DateTimeOffset.Now,
						CrowdSourcedFieldVerifiedEntryValues = new List<Models.CrowdSourcedFieldVerifiedEntryValue>(),
					};
					_context.CrowdSourcedFieldVerifiedEntries.Add(verifiedEntry);
					await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateAddVerifiedEntryLog(comic, verifiedEntry));

					// Remove old verified entry values and add new ones.
					var verifiedEntryValues = firstUserEntry.CrowdSourcedFieldUserEntryValues
						.Select(csfuev => new Models.CrowdSourcedFieldVerifiedEntryValue()
						{
							ComicId = comic.Id,
							CrowdSourcedFieldDefinitionId = fieldDefinition.Id,
							Id = csfuev.Id,
							Value = csfuev.Value
						})
						.ToList();
					_context.CrowdSourcedFieldVerifiedEntryValues.AddRange(verifiedEntryValues);

					// Save changes
					await _context.SaveChangesAsync();
				}
			}
			else
			{
				// Threshold not met, remove any VerifiedEntry previously added.
				if (verifiedEntry != null)
				{
					verification = false;

					_context.CrowdSourcedFieldVerifiedEntryValues.RemoveRange(verifiedEntry.CrowdSourcedFieldVerifiedEntryValues);
					_context.CrowdSourcedFieldVerifiedEntries.Remove(verifiedEntry);
					await _context.ComicHistoryLogs.AddAsync(_historyLogger.CreateRemoveVerifiedEntryLog(comic, verifiedEntry));

					await _context.SaveChangesAsync();
				}
			}

			return verification;
		}
	}
}
