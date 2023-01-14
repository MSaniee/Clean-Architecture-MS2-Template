namespace MS2Project.Domain.CustomerAggregate.Orders.Events;

public class OrderRemovedEvent : DomainEventBase
{
    public OrderId OrderId { get; }

    public OrderRemovedEvent(OrderId orderId)
    {
        OrderId = orderId;
    }
}

