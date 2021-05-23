using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Blazored.Toast;

using BlazorTable;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

using Blazored.LocalStorage;

namespace LoadingArtistCrowdSource.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddHttpClient("LoadingArtistCrowdSource.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LoadingArtistCrowdSource.ServerAPI"));

			builder.Services.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<Services.RolesClaimsPrincipalFactory>();

			// Custom
			builder.Services.AddHttpClient("LoadingArtistCrowdSource.PublicServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
			builder.Services.AddScoped<Services.LACSApi>();
			builder.Services.AddBlazorTable();

			// Blazorise
			builder.Services
				.AddBlazorise(options =>
				{
					options.ChangeTextOnKeyPress = true;
				})
				.AddBootstrapProviders();
				
			// Toast
			builder.Services.AddBlazoredToast();

			// Blazored.LocalStorage
			builder.Services.AddBlazoredLocalStorage();

			// Filter console logs
			builder.Logging.SetMinimumLevel(LogLevel.Warning);

			var host = builder.Build();

			// Blazorise
			host.Services
				.UseBootstrapProviders()
				.UseFontAwesomeIcons();

			await host.RunAsync();
		}
	}
}
