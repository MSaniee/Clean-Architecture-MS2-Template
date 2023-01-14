using MS2Project.Domain.Core.Bases;
using MS2Project.Domain.CustomerAggregate.Orders;
using MS2Project.Domain.PaymentAggregate.Events;

namespace MS2Project.Domain.PaymentAggregate;

public class Payment : BaseEntity<PaymentId>, IAggregateRoot
{

    private OrderId _orderId;

    private DateTime _createDate;

    private PaymentStatus _status;

    private bool _emailNotificationIsSent;

    private Payment()
    {
        // Only for EF.
    }

    public Payment(OrderId orderId)
    {
        Id = new PaymentId(Guid.NewGuid());
        _createDate = DateTime.UtcNow;
        _orderId = orderId;
        _status = PaymentStatus.ToPay;
        _emailNotificationIsSent = false;

        AddDomainEvent(new PaymentCreatedEvent(Id, _orderId));
    }

    public void MarkEmailNotificationIsSent()
    {
        _emailNotificationIsSent = true;
    }
}

public class PaymentId : TypedIdValueBase
{
    //private PaymentId() { }
    public PaymentId(Guid value) : base(value)
    {
    }
}