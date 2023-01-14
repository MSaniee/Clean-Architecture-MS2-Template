using MS2Project.Domain.CustomerAggregate.Orders;

namespace MS2Project.Domain.CustomerAggregate.Rules;

public class OrderMustHaveAtLeastOneProductRule : IBusinessRule
{
    private readonly List<OrderProductData> _orderProductData;

    public OrderMustHaveAtLeastOneProductRule(List<OrderProductData> orderProductData)
    {
        _orderProductData = orderProductData;
    }

    public bool IsBroken() => !_orderProductData.Any();

    public string Message => Memos.OrderMustHaveAtLeastOneProduct;
}
