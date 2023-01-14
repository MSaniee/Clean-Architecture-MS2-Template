namespace MS2Project.Domain.CustomerAggregate.Orders.Events;

public class OrderChangedEvent : DomainEventBase
{
    public OrderId OrderId { get; }

    public OrderChangedEvent(OrderId orderId)
    {
        OrderId = orderId;
    }
}

