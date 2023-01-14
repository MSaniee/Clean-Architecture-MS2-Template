using MS2Project.Application.Bases.DomainEvents;
using MS2Project.Application.Bases.Processing;
using MS2Project.Application.Features.Payments.Commands;
using MS2Project.Domain.PaymentAggregate;
using MS2Project.Domain.PaymentAggregate.Events;

namespace MS2Project.Application.Features.Payments.Notifications;

public class PaymentCreatedNotification : DomainNotificationBase<PaymentCreatedEvent>
{
    public PaymentId PaymentId { get; }

    public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
    {
        PaymentId = domainEvent.PaymentId;
    }

    [JsonConstructor]
    public PaymentCreatedNotification(PaymentId paymentId) : base(null)
    {
        PaymentId = paymentId;
    }
}

public class PaymentCreatedNotificationHandler : INotificationHandler<PaymentCreatedNotification>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PaymentCreatedNotificationHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler.ThrowIfNull();
    }

    public async Task Handle(PaymentCreatedNotification request, CancellationToken cancellationToken)
    {
        await _commandsScheduler.EnqueueAsync(
            new SendEmailAfterPaymentCommand(Guid.NewGuid(), request.PaymentId));
    }
}
