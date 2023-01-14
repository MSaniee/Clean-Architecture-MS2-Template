using MS2Project.Domain.Core.Bases;

namespace MS2Project.Domain.CustomerAggregate.Orders;

public partial class Order : Entity
{
    internal OrderId Id;

    private bool _isRemoved;

    private MoneyValue _value;

    private MoneyValue _valueInEUR;

    private List<OrderProduct> _orderProducts;

    private OrderStatus _status;

    private DateTime _orderDate;

    private DateTime? _orderChangeDate;
}

