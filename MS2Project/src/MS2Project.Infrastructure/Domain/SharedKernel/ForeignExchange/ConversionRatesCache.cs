using MS2Project.Domain.SharedKernel.ForeignExchange;

namespace MS2Project.Infrastructure.Domain.SharedKernel.ForeignExchange;

public class ConversionRatesCache
{
    public List<ConversionRate> Rates { get; }

    public ConversionRatesCache(List<ConversionRate> rates)
    {
        Rates = rates;
    }
}
