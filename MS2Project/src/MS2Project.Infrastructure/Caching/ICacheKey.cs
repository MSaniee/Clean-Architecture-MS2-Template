namespace MS2Project.Infrastructure.Caching;

public interface ICacheKey<TItem>
{
    string CacheKey { get; }
}
