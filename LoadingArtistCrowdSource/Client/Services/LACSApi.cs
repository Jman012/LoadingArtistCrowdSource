using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using LoadingArtistCrowdSource.Shared.Enums;
using LoadingArtistCrowdSource.Shared.Models;
using LoadingArtistCrowdSource.Shared.Utilities;

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
		public async Task<ComicListItemViewModel[]> GetComics()
		{
			return await _publicClient.GetFromJsonAsync<ComicListItemViewModel[]>("api/comic", _serializationOptions) ?? new ComicListItemViewModel[] { };
		}
		public async Task<ComicPageViewModel> GetComic(string code)
		{
			return await _publicClient.GetFromJsonAsync<ComicPageViewModel>($"api/comic/{Uri.EscapeDataString(code)}", _serializationOptions) ?? new ComicPageViewModel();
		}
		public async Task<UserEntrySubmissionResult> PutUserEntryValues(string comicCode, string fieldCode, List<string> values)
		{
			return await _authClient.PutAsJsonAsync<List<string>, UserEntrySubmissionResult>($"api/comic/{Uri.EscapeDataString(comicCode)}/entry/{Uri.EscapeDataString(fieldCode)}", values, _serializationOptions);
		}
		public async Task PutComicMetadata(string comicCode, ComicViewModel vm)
		{
			var response = await _authClient.PutAsJsonAsync($"api/comic/{Uri.EscapeDataString(comicCode)}/edit", vm, _serializationOptions);
			response.EnsureSuccessStatusCode();
		}
		#endregion

		#region FieldController
		public async Task<CrowdSourcedFieldDefinitionViewModel[]> GetFields()
		{
			return await _authClient.GetFromJsonAsync<CrowdSourcedFieldDefinitionViewModel[]>("api/field", _serializationOptions) ?? new CrowdSourcedFieldDefinitionViewModel[] { };
		}
		public async Task<FieldDefinitionFormViewModel> GetField(string code)
		{
			return await _authClient.GetFromJsonAsync<FieldDefinitionFormViewModel>($"api/field/{Uri.EscapeDataString(code)}", _serializationOptions) ?? new FieldDefinitionFormViewModel();
		}
		public async Task<string?> PutField(FieldDefinitionFormViewModel vm)
		{
			var response = await _authClient.PutAsJsonAsync($"api/field/{Uri.EscapeDataString(vm.Code)}", vm, _serializationOptions);
			if (response.IsSuccessStatusCode)
			{
				return null;
			}

			return await response.Content.ReadAsStringAsync();
		}
		public async Task<string?> PutFieldPositions(string[] vm)
		{
			var response = await _authClient.PutAsJsonAsync($"api/field_positions/", vm, _serializationOptions);
			if (response.IsSuccessStatusCode)
			{
				return null;
			}

			return await response.Content.ReadAsStringAsync();
		}
		#endregion

		#region SearchController
		public async Task<ComicFieldViewModel[]> GetSearchFields()
		{
			return await _publicClient.GetFromJsonAsync<ComicFieldViewModel[]>("api/search/fields", _serializationOptions) ?? new ComicFieldViewModel[] { };
		}
		public async Task<ComicListItemViewModel[]> PostSearch(SearchViewModel vm)
		{
			return await _publicClient.PostAsJsonAsync<SearchViewModel, ComicListItemViewModel[]>("api/search", vm, _serializationOptions) ?? new ComicListItemViewModel[] { };
		}
		#endregion

		#region RandomController
		public async Task<string> GetRandomComicCode()
		{
			return await _publicClient.GetStringAsync("api/random");
		}
		#endregion RandomController

		#region AdminController
		public async Task<string> ImportFeed(Stream ms)
		{
			var content = new MultipartFormDataContent
			{
				{ new StreamContent(ms), "\"upload\"", "feed.xml" }
			};
			var response = await _authClient.PostAsync("api/admin/import_feed", content);
			return await response.Content.ReadAsStringAsync();
		}
		#endregion AdminController
	}
}
