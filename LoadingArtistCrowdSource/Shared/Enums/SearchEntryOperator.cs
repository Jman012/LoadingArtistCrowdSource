using System;

namespace LoadingArtistCrowdSource.Shared.Enums
{
	public enum SearchEntryOperator
	{
		Any,
		All,
	}

	public static class SearchEntryOperatorExtensions
	{
		public static string Separator(this SearchEntryOperator @this)
		{
			switch (@this)
			{
				case SearchEntryOperator.Any: return ", or ";
				case SearchEntryOperator.All: return ", and ";
				default: return ", ";
			}
		}
	}
}