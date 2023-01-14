using MS2Project.Domain.SharedKernel.Money;

namespace MS2Project.Domain.ProductAggregate;

public class ProductPrice
{
    public MoneyValue Value { get; private set; }

    private ProductPrice()
    {

    }
}


