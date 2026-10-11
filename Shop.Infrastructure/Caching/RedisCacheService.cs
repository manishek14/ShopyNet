using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Shop.Application.Caching;

namespace Shop.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _connection;
        private readonly RedisSettings _settings;

        public RedisCacheService(
            IDistributedCache cache,
            IConnectionMultiplexer connection,
            IOptions<RedisSettings> settings)
        {
            _cache = cache;
            _connection = connection;
            _settings = settings.Value;
        }

        // GetAsync
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var cachedValue = await _cache.GetStringAsync(BuildKey(key), cancellationToken);

            if (string.IsNullOrEmpty(cachedValue))
                return default;

            return JsonSerializer.Deserialize<T>(cachedValue);
        }

        // SetAsync
        public async Task SetAsync<T>(
            string key,
            T value,
            TimeSpan? expiry = null,
            CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(_settings.DefaultExpiryMinutes)
            };

            var serializedValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(BuildKey(key), serializedValue, options, cancellationToken);
        }

        // RemoveAsync
        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(BuildKey(key), cancellationToken);
        }

        // ExistsAsync
        public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        {
            var value = await _cache.GetStringAsync(BuildKey(key), cancellationToken);
            return !string.IsNullOrEmpty(value);
        }

        // GetOrSetAsync (Pattern)
        public async Task<T> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> factory,
            TimeSpan? expiry = null,
            CancellationToken cancellationToken = default)
        {
            var cachedValue = await GetAsync<T>(key, cancellationToken);
            if (cachedValue != null)
                return cachedValue;

            var value = await factory();
            if (value != null)
                await SetAsync(key, value, expiry, cancellationToken);

            return value;
        }

        // RemoveByPatternAsync
        public async Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
        {
            var endpoints = _connection.GetEndPoints();
            var server = _connection.GetServer(endpoints.First());

            var keys = server.Keys(pattern: $"{_settings.InstanceName}:{pattern}").ToArray();

            if (keys.Any())
            {
                var db = _connection.GetDatabase();
                await db.KeyDeleteAsync(keys);
            }
        }

        // BuildKey (With InstanceName)
        private string BuildKey(string key)
        {
            return $"{_settings.InstanceName}:{key}";
        }
    }
}