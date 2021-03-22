using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class JsonDistributedCache<TCategoryName>
	{
		private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
		private readonly IDistributedCache _distCache;
		private readonly ILogger<JsonDistributedCache<TCategoryName>> _logger;

		public JsonDistributedCache(IDistributedCache distCache, ILogger<JsonDistributedCache<TCategoryName>> logger)
		{
			_distCache = distCache;
			_logger = logger;
		}

		public async Task<T> GetAsync<T>(string key, Func<T> defaultFactory)
		{
			T? value = await GetAsync<T>(key);
			if (value == null)
			{
				_logger.LogDebug($"Creating default value {typeof(T).FullName} for key '{key}'");
				value = defaultFactory();
				await SetAsync(key, value);
			}

			return value;
		}

		public async Task<T> GetAsync<T>(string key, Func<Task<T>> defaultFactory)
		{
			T? value = await GetAsync<T>(key);
			if (value == null)
			{
				_logger.LogDebug($"Creating default value {typeof(T).FullName} for key '{key}'");
				value = await defaultFactory();
				await SetAsync(key, value);
			}

			return value;
		}

		public async Task<T> GetAsync<T>(string key, Func<(T, DistributedCacheEntryOptions)> defaultFactory)
		{
			T? value = await GetAsync<T>(key);
			if (value == null)
			{
				_logger.LogDebug($"Creating default value {typeof(T).FullName} for key '{key}'");
				var newValueTuple = defaultFactory();
				value = newValueTuple.Item1;
				var cacheEntryOptions = newValueTuple.Item2;
				await SetAsync(key, value, cacheEntryOptions);
			}

			return value;
		}

		public async Task<T> GetAsync<T>(string key, Func<Task<(T, DistributedCacheEntryOptions)>> defaultFactory)
		{
			T? value = await GetAsync<T>(key);
			if (value == null)
			{
				_logger.LogDebug($"Creating default value {typeof(T).FullName} for key '{key}'");
				var newValueTuple = await defaultFactory();
				value = newValueTuple.Item1;
				var cacheEntryOptions = newValueTuple.Item2;
				await SetAsync(key, value, cacheEntryOptions);
			}

			return value;
		}

		public async Task<T?> GetAsync<T>(string key)
		{
			byte[] bytes = await _distCache.GetAsync(key);
			if (bytes == null || bytes.Length == 0)
			{
				_logger.LogDebug($"Key '{key}' cache miss for {typeof(T).FullName}");
				return default;
			}
			return JsonSerializer.Deserialize<T>(bytes, _serializerOptions);
		}

		public async Task SetAsync<T>(string key, T value)
		{
			byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, _serializerOptions);
			await _distCache.SetAsync(key, bytes);
			_logger.LogDebug($"Set key '{key}' for {typeof(T).FullName} with {bytes.Length} bytes");
		}

		public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
		{
			byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, _serializerOptions);
			await _distCache.SetAsync(key, bytes, options);
			_logger.LogDebug($"Set key '{key}' for {typeof(T).FullName} with {bytes.Length} bytes, expiring {CacheEntryOptionsDescription(options)}");
		}

		public async Task RemoveAsync(string key)
		{
			_logger.LogDebug($"Removing key {key}");
			await _distCache.RemoveAsync(key);
		}

		private string CacheEntryOptionsDescription(DistributedCacheEntryOptions options)
		{
			if (options.AbsoluteExpiration.HasValue)
			{
				return "on " + options.AbsoluteExpiration.Value.ToString();
			}
			else if (options.AbsoluteExpirationRelativeToNow.HasValue)
			{
				return "in " + options.AbsoluteExpirationRelativeToNow.Value.ToString();
			}
			else if (options.SlidingExpiration.HasValue)
			{
				return "in (sliding) " + options.SlidingExpiration.Value.ToString();
			}
			return "never";
		}
	}
}