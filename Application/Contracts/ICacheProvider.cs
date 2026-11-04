namespace Application.Contracts;

public interface ICacheProvider
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(T obj, string key, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
}