using MS2Project.Domain.ProductAggregate;

namespace MS2Project.Domain.CustomerAggregate.Orders;

public class OrderProductData
{
    public OrderProductData(ProductId productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public ProductId ProductId { get; }

    public int Quantity { get; }
}

