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
		public LACSApi(IHttpClientFactory httpClientFactory, HttpClient authClient)
		{
			_authClient = authClient;
			//_authClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.ServerAPI");
			_publicClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.PublicServerAPI");
		}

		#region ComicController
		public async Task<ComicViewModel[]> GetComics()
		{
			return await _publicClient.GetFromJsonAsync<ComicViewModel[]>("api/comic") ?? new ComicViewModel[] { };
		}
		public async Task<ComicViewModel> GetComic(string code)
		{
			return await _publicClient.GetFromJsonAsync<ComicViewModel>($"api/comic/{code}") ?? new ComicViewModel();
		}
		#endregion

		#region FieldController
		public async Task<CrowdSourcedFieldDefinitionViewModel[]> GetFields()
		{
			return await _authClient.GetFromJsonAsync<CrowdSourcedFieldDefinitionViewModel[]>("api/field") ?? new CrowdSourcedFieldDefinitionViewModel[] { };
		}
		public async Task<CrowdSourcedFieldDefinitionViewModel> GetField(string code)
		{
			return await _authClient.GetFromJsonAsync<CrowdSourcedFieldDefinitionViewModel>($"api/field/{code}") ?? new CrowdSourcedFieldDefinitionViewModel();
		}
		#endregion
	}
}
