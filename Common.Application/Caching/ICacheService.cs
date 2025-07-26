namespace Common.Application.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    Task<byte[]> GetAsync(string key, CancellationToken cancellationToken = default);

    Task SetAsync(
        string key,
        byte[] value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}