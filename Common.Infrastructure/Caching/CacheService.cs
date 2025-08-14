using Common.Application.Caching;
using Common.Helper;
using Microsoft.Extensions.Caching.Distributed;

namespace Common.Infrastructure.Caching;

internal sealed class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var bytes = await cache.GetAsync(key, cancellationToken);
        if (bytes != null) await SetAsync(key, bytes);
        return bytes is null ? default : BufferHelper.Deserialize<T>(bytes);
    }

    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        var bytes = BufferHelper.Serialize(value);
        return cache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
    }

    public async Task<byte[]?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var bytes = await cache.GetAsync(key, cancellationToken);
        if (bytes != null) await SetAsync(key, bytes);
        return bytes;
    }

    public Task SetAsync(
        string key,
        byte[] value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        return cache.SetAsync(key, value, CacheOptions.Create(expiration), cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return cache.RemoveAsync(key, cancellationToken);
    }
}