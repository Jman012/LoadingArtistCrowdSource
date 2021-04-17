using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

using Microsoft.Extensions.Configuration;

namespace LoadingArtistCrowdSource.Server.Services
{
	public static class ServerConfig
	{
		public const string Discord_ClientID = "Discord:ClientId";
		public const string Discord_ClientSecret = "Discord:ClientSecret";

		public const string MailJet_ApiKey = "MailJet:ApiKey";
		public const string MailJet_ApiSecret = "MailJet:ApiSecret";

		public const string LACS_FromEmailAddress = "LACS:FromEmailAddress";
		public const string LACS_FromEmailName = "LACS:FromEmailName";

		public const string LACS_LoadingArtistDomain = "LACS:LoadingArtistDomain";

		public static void AssertConfigAvailable(IConfiguration configuration)
		{
			var fields = typeof(ServerConfig).GetFields(BindingFlags.Public | BindingFlags.Static);
			var missingFields = new List<string>();
			foreach (var field in fields)
			{
				string configKey = (string)field.GetValue(null)!;
				string configValue = configuration.GetValue<string>(configKey);
				if (string.IsNullOrEmpty(configValue))
				{
					missingFields.Add(configKey);
				}
			}

			if (missingFields.Count > 0)
			{
				throw new Exception($"Missing {typeof(ServerConfig).Assembly.FullName} configuration keys: {string.Join(", ", missingFields)}");
			}
		}
	}
}
