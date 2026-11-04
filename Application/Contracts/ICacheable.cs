namespace Application.Contracts;

public interface ICacheable
{
    string GetCacheKey();
    TimeSpan CacheDuration { get; }
}