using System.Text.Json;
using Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Cache;

public class CacheProvider(IDistributedCache cache) : ICacheProvider
{
    public async Task<T?> GetAsync<T>(string key)
    {
        var cached = await cache.GetStringAsync(key);
        if (string.IsNullOrEmpty(cached))
            return default;
        return JsonSerializer.Deserialize<T>(cached);
    }

    public async Task SetAsync<T>(T obj, string key, TimeSpan? expiration = null)
    {
        var cacheOptions = new DistributedCacheEntryOptions();
        
        if (expiration.HasValue)
            cacheOptions.SetAbsoluteExpiration(expiration.Value);
        else
            cacheOptions.SetSlidingExpiration(TimeSpan.FromMinutes(5));
        
        await cache.SetStringAsync(key, JsonSerializer.Serialize(obj), cacheOptions);
    }

    public async Task RemoveAsync(string key)
    {
        await cache.RemoveAsync(key);
    }
}