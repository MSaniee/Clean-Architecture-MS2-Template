using MS2Project.Domain.CustomerAggregate.Orders;

namespace MS2Project.Domain.PaymentAggregate.Events;

public class PaymentCreatedEvent : DomainEventBase
{
    public PaymentCreatedEvent(PaymentId paymentId, OrderId orderId)
    {
        PaymentId = paymentId;
        OrderId = orderId;
    }

    public PaymentId PaymentId { get; }

    public OrderId OrderId { get; }
}

