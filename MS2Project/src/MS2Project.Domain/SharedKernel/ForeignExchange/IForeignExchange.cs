namespace MS2Project.Domain.SharedKernel.ForeignExchange;

public interface IForeignExchange
{
    List<ConversionRate> GetConversionRates();
}
