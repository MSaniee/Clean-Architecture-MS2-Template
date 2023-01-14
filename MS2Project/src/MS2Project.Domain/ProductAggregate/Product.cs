using MS2Project.Domain.Core.Bases;

namespace MS2Project.Domain.ProductAggregate;

public partial class Product : BaseEntity<ProductId>, IAggregateRoot
{
    public string Name { get; private set; }

    private List<ProductPrice> _prices;

    private Product() { }
}

