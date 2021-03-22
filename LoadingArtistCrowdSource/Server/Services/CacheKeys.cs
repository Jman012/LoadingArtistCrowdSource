using System;

namespace LoadingArtistCrowdSource.Server.Services
{
	public static class CacheKeys
	{
		public static _LACS LACS = new _LACS();
		public class _LACS 
		{
			public string ComicIndex => "LACS.ComicIndex";
			public string GetComic(string comicCode) => $"LACS.GetComic.{comicCode}";

			public _Stats Stats = new _Stats();
			public class _Stats
			{
				public string Index => "LACS.Stats.Index";
			}

			public _Tags Tags = new _Tags();
			public class _Tags
			{
				public string Index => "LACS.Tags.Index";
			}
		}
	}
}