using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using LoadingArtistCrowdSource.Shared.Models;

namespace LoadingArtistCrowdSource.Client.Services
{
	public class LACSApi
	{
		private HttpClient _authClient { get; }
		private HttpClient _publicClient { get; }
		public LACSApi(IHttpClientFactory httpClientFactory)
		{
			_authClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.ServerAPI");
			_publicClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.PublicServerAPI");
		}

		#region ComicListController
		public async Task<ComicViewModel[]> GetComics()
		{
			return await _publicClient.GetFromJsonAsync<ComicViewModel[]>("api/comics") ?? new ComicViewModel[] { };
		}
		public async Task<ComicViewModel> GetComic(string code)
		{
			return await _publicClient.GetFromJsonAsync<ComicViewModel>($"api/comics/{code}") ?? new ComicViewModel();
		}
		#endregion
	}
}
