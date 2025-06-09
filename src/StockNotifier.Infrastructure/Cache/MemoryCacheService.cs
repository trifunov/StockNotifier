using Microsoft.Extensions.Caching.Memory;
using StockNotifier.Application.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public ValueTask<T?> GetAsync<T>(string key)
        {
            var value = _memoryCache.Get<byte[]>(key);
            T? deserializedValue = value != null ? JsonSerializer.Deserialize<T>(value) : default;
            return ValueTask.FromResult(deserializedValue);
        }

        public ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var serializedValue = JsonSerializer.SerializeToUtf8Bytes(value);
            _memoryCache.Set(key, serializedValue, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(60) // Default to 60 minutes if no expiration is provided
            });
            return ValueTask.CompletedTask;
        }
    }
}
