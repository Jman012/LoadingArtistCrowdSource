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
		private System.Text.Json.JsonSerializerOptions _serializationOptions { get; }
		public LACSApi(IHttpClientFactory httpClientFactory, HttpClient authClient)
		{
			_authClient = authClient;
			//_authClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.ServerAPI");
			_publicClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.PublicServerAPI");

			_serializationOptions = new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web);
			_serializationOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
		}

		#region ComicController
		public async Task<ComicViewModel[]> GetComics()
		{
			return await _publicClient.GetFromJsonAsync<ComicViewModel[]>("api/comic", _serializationOptions) ?? new ComicViewModel[] { };
		}
		public async Task<ComicViewModel> GetComic(string code)
		{
			return await _publicClient.GetFromJsonAsync<ComicViewModel>($"api/comic/{code}", _serializationOptions) ?? new ComicViewModel();
		}
		#endregion

		#region FieldController
		public async Task<CrowdSourcedFieldDefinitionViewModel[]> GetFields()
		{
			return await _authClient.GetFromJsonAsync<CrowdSourcedFieldDefinitionViewModel[]>("api/field", _serializationOptions) ?? new CrowdSourcedFieldDefinitionViewModel[] { };
		}
		public async Task<FieldDefinitionFormViewModel> GetField(string code)
		{
			return await _authClient.GetFromJsonAsync<FieldDefinitionFormViewModel>($"api/field/{code}", _serializationOptions) ?? new FieldDefinitionFormViewModel();
		}
		#endregion
	}
}
