using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
		private readonly HttpClient _authClient;
		private readonly HttpClient _publicClient;
		private readonly JsonSerializerOptions _serializationOptions;
		public LACSApi(IHttpClientFactory httpClientFactory, HttpClient authClient)
		{
			_authClient = authClient;
			_publicClient = httpClientFactory.CreateClient("LoadingArtistCrowdSource.PublicServerAPI");

			_serializationOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
			_serializationOptions.Converters.Add(new JsonStringEnumConverter());
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
		public async Task<TranscriptHistoryItemViewModel[]> GetTranscriptHistory(string comicCode)
		{
			return await _publicClient.GetFromJsonAsync<TranscriptHistoryItemViewModel[]>($"api/comic/{Uri.EscapeDataString(comicCode)}/transcript", _serializationOptions) ?? new TranscriptHistoryItemViewModel[] { };
		}
		public async Task<string?> PostTranscriptHistory(string comicCode, TranscriptViewModel vm)
		{
			var response = await _authClient.PostAsJsonAsync($"api/comic/{Uri.EscapeDataString(comicCode)}/transcript", vm, _serializationOptions);
			if (response.IsSuccessStatusCode)
			{
				return null;
			}

			return await response.Content.ReadAsStringAsync();
		}
		public async Task<string?> PostTranscriptRollbackToHistory(string comicCode, int id)
		{
			var response = await _authClient.PostAsync($"api/comic/{Uri.EscapeDataString(comicCode)}/transcript/{id}", new StringContent(""));
			if (response.IsSuccessStatusCode)
			{
				return null;
			}

			return await response.Content.ReadAsStringAsync();
		}
		public async Task<ComicHistoryLogViewModel> GetComicHistory(string comicCode)
		{
			return await _authClient.GetFromJsonAsync<ComicHistoryLogViewModel>($"api/comic/{Uri.EscapeDataString(comicCode)}/history", _serializationOptions) ?? new ComicHistoryLogViewModel();
		}
		public async Task<string?> PutComicTags(string comicCode, ComicTagsViewModel vm)
		{
			var response = await _authClient.PutAsJsonAsync($"api/comic/{Uri.EscapeDataString(comicCode)}/tags", vm, _serializationOptions);
			if (response.IsSuccessStatusCode)
			{
				return null;
			}

			return await response.Content.ReadAsStringAsync();
		}
		public async Task<ComicListItemViewModel[]> GetComicIntegrityQueue()
		{
			return await _publicClient.GetFromJsonAsync<ComicListItemViewModel[]>("api/comic/integrity_queue", _serializationOptions) ?? new ComicListItemViewModel[] { };
		}
		#endregion ComicController

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
		public async Task<CrowdSourcedFieldDefinitionHistoryLogViewModel> GetFieldHistory(string fieldCode)
		{
			return await _authClient.GetFromJsonAsync<CrowdSourcedFieldDefinitionHistoryLogViewModel>($"api/field/{Uri.EscapeDataString(fieldCode)}/history", _serializationOptions) ?? new CrowdSourcedFieldDefinitionHistoryLogViewModel();
		}
		#endregion FieldController

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
		public async Task<string> ImportInitialComics(Stream ms)
		{
			var content = new MultipartFormDataContent
			{
				{ new StreamContent(ms), "\"upload\"", "feed.xml" }
			};
			var response = await _authClient.PostAsync("api/admin/import_initial_comics", content);
			return await response.Content.ReadAsStringAsync();
		}

		public async Task<string> ImportNewComics()
		{
			var response = await _authClient.PostAsync("api/admin/import_comics", new StringContent(""));
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}
		public async Task<string> SetUserRoles(AdminSetUserRolesViewModel vm)
		{
			var response = await _authClient.PutAsJsonAsync($"api/admin/user/{Uri.EscapeDataString(vm.Username)}/roles", vm.UserRoles, _serializationOptions);
			return await response.Content.ReadAsStringAsync();
		}
		public async Task<string> ExportFields()
		{
			var response = await _authClient.GetAsync("api/admin/export_fields");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}
		public async Task<string> ImportFields(string json)
		{
			var response = await _authClient.PostAsync("api/admin/import_fields", new StringContent(json));
			return await response.Content.ReadAsStringAsync();
		}
		#endregion AdminController

		#region FeedbackController
		public async Task<FeedbackViewModel[]> GetFeedbackList()
		{
			return await _authClient.GetFromJsonAsync<FeedbackViewModel[]>("api/feedback", _serializationOptions) ?? new FeedbackViewModel[] { };
		}
		public async Task<string?> PostFeedback(FeedbackViewModel vm)
		{
			var response = await _authClient.PostAsJsonAsync($"/api/feedback/{Uri.EscapeDataString(vm.ComicCode)}/{Uri.EscapeDataString(vm.FieldCode)}", vm, _serializationOptions);
			if (!response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsStringAsync();
			}

			return null;
		}
		public async Task<FeedbackViewModel[]> GetFeedbackQueueList()
		{
			return await _authClient.GetFromJsonAsync<FeedbackViewModel[]>("api/feedback/queue", _serializationOptions) ?? new FeedbackViewModel[] { };
		}
		public async Task<FeedbackViewModel> GetFeedback(string comicCode, string fieldCode, int id)
		{
			return await _authClient.GetFromJsonAsync<FeedbackViewModel>($"api/feedback/{Uri.EscapeDataString(comicCode)}/{Uri.EscapeDataString(fieldCode)}/{id}", _serializationOptions) ?? new FeedbackViewModel();
		}
		public async Task<string?> PostCompleteFeedback(string comicCode, string fieldCode, int id, FeedbackViewModel vm)
		{
			var response = await _authClient.PostAsJsonAsync($"api/feedback/{Uri.EscapeDataString(vm.ComicCode)}/{Uri.EscapeDataString(vm.FieldCode)}/{id}", vm, _serializationOptions);
			if (!response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsStringAsync();
			}

			return null;
		}
		#endregion FeedbackController

		#region StatisticsController
		public async Task<StatisticsViewModel> GetStatistics()
		{
			return await _publicClient.GetFromJsonAsync<StatisticsViewModel>("api/statistics", _serializationOptions) ?? new StatisticsViewModel();
		}
		#endregion StatisticsController

		#region TagController
		public async Task<ComicTagsViewModel> GetSystemTags()
		{
			return await _publicClient.GetFromJsonAsync<ComicTagsViewModel>("api/tag", _serializationOptions) ?? new ComicTagsViewModel();
		}
		#endregion TagController
	}
}
