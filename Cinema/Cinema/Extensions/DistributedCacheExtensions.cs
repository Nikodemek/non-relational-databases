using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Extensions;

public static class DistributedCacheExtensions
{
    private static readonly TimeSpan DefaultAbsoluteExpirationTime = TimeSpan.FromDays(30);
    
    public static async Task SetRecordAsync<T>(
        this IDistributedCache cache,
        string key,
        T data,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? unusedExpireTime = null,
        CancellationToken token = default)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? DefaultAbsoluteExpirationTime,
            SlidingExpiration = unusedExpireTime,
        };

        string serializedData = JsonSerializer.Serialize<T>(data);
        await cache.SetStringAsync(GetPrefixedId<T>(key), serializedData, options, token);
    }

    public static async Task<T?> GetRecordAsync<T>(
        this IDistributedCache cache,
        string key,
        CancellationToken token = default)
    {
        return await cache.GetStringAsync(GetPrefixedId<T>(key), token) is string serializedData
            ? JsonSerializer.Deserialize<T>(serializedData)
            : default;
    }
    
    public static async Task<T?[]> GetRecordsAsync<T>(
        this IDistributedCache cache,
        ICollection<string> keys,
        CancellationToken token = default)
    {
        var tasks = keys.Select(key => cache.GetRecordAsync<T>(key, token));
        T?[] results = await Task.WhenAll(tasks);

        return results;
    }

    public static async Task RemoveRecordAsync<T>(
        this IDistributedCache cache,
        string key,
        CancellationToken token = default)
    {
        await cache.RemoveAsync(GetPrefixedId<T>(key), token);
    }

    private static string GetPrefixedId<T>(string id) => $"{typeof(T).Name}_{id}";
}