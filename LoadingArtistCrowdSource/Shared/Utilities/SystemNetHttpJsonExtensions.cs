using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace System.Net.Http.Json
{
	public static class SystemNetHttpJsonExtensions
	{
		#region Put
		public static Task<TResponse?> PutAsJsonAsync<TValue, TResponse>(this HttpClient client, string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		{
			if (client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}

			Task<HttpResponseMessage> taskResponse = client.PutAsJsonAsync(requestUri, value, options, cancellationToken);
			return GetFromJsonAsyncCore<TResponse>(taskResponse, options, cancellationToken);
		}
		public static Task<TResponse?> PutAsJsonAsync<TValue, TResponse>(this HttpClient client, string? requestUri, TValue value, CancellationToken cancellationToken)
			=> PutAsJsonAsync<TValue, TResponse>(client, requestUri, value, options: null, cancellationToken);
		public static Task<TResponse?> PutAsJsonAsync<TValue, TResponse>(this HttpClient client, Uri? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		{
			if (client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}

			Task<HttpResponseMessage> taskResponse = client.PutAsJsonAsync(requestUri, value, options, cancellationToken);
			return GetFromJsonAsyncCore<TResponse>(taskResponse, options, cancellationToken);
		}
		public static Task<TResponse?> PutAsJsonAsync<TValue, TResponse>(this HttpClient client, Uri? requestUri, TValue value, CancellationToken cancellationToken)
			=> PutAsJsonAsync<TValue, TResponse>(client, requestUri, value, options: null, cancellationToken);
		#endregion Put

		#region Post
		public static Task<TResponse?> PostAsJsonAsync<TValue, TResponse>(this HttpClient client, string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		{
			if (client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}

			Task<HttpResponseMessage> taskResponse = client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
			return GetFromJsonAsyncCore<TResponse>(taskResponse, options, cancellationToken);
		}
		public static Task<TResponse?> PostAsJsonAsync<TValue, TResponse>(this HttpClient client, string? requestUri, TValue value, CancellationToken cancellationToken)
			=> PostAsJsonAsync<TValue, TResponse>(client, requestUri, value, options: null, cancellationToken);
		public static Task<TResponse?> PostAsJsonAsync<TValue, TResponse>(this HttpClient client, Uri? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		{
			if (client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}

			Task<HttpResponseMessage> taskResponse = client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
			return GetFromJsonAsyncCore<TResponse>(taskResponse, options, cancellationToken);
		}
		public static Task<TResponse?> PostAsJsonAsync<TValue, TResponse>(this HttpClient client, Uri? requestUri, TValue value, CancellationToken cancellationToken)
			=> PostAsJsonAsync<TValue, TResponse>(client, requestUri, value, options: null, cancellationToken);
		#endregion Post

		private static async Task<T?> GetFromJsonAsyncCore<T>(Task<HttpResponseMessage> taskResponse, JsonSerializerOptions? options, CancellationToken cancellationToken)
		{
			using (HttpResponseMessage response = await taskResponse.ConfigureAwait(false))
			{
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<T>(options, cancellationToken).ConfigureAwait(false);
			}
		}
	}
}
