namespace MS2Project.Domain.ProductAggregate;

public partial class Product
{
}

public class ProductId : TypedIdValueBase
{
    public ProductId(Guid value) : base(value)
    {
    }
}