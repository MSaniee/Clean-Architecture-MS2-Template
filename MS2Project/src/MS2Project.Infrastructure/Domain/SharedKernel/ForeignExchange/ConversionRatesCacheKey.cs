using MS2Project.Infrastructure.Caching;

namespace MS2Project.Infrastructure.Domain.SharedKernel.ForeignExchange;

public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
{
    public string CacheKey => "ConversionRatesCache";
}

