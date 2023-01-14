using MS2Project.Domain.SharedKernel.ForeignExchange;
using MS2Project.Infrastructure.Caching;

namespace MS2Project.Infrastructure.Domain.SharedKernel.ForeignExchange;

public class ForeignExchange : IForeignExchange , IScopedDependency
{
    private readonly ICacheStore _cacheStore;

    public ForeignExchange(ICacheStore cacheStore)
    {
        _cacheStore = cacheStore;
    }

    public List<ConversionRate> GetConversionRates()
    {
        var ratesCache = _cacheStore.Get(new ConversionRatesCacheKey());

        if (ratesCache != null)
        {
            return ratesCache.Rates;
        }

        List<ConversionRate> rates = GetConversionRatesFromExternalApi();

        _cacheStore.Add(new ConversionRatesCache(rates), new ConversionRatesCacheKey(), DateTime.Now.Date.AddDays(1));

        return rates;
    }

    private static List<ConversionRate> GetConversionRatesFromExternalApi()
    {
        // Communication with external API. Here is only mock.

        return new()
        {
            new ConversionRate("USD", "EUR", (decimal)0.88),
            new ConversionRate("EUR", "USD", (decimal)1.13)
        }; ;
    }
}

