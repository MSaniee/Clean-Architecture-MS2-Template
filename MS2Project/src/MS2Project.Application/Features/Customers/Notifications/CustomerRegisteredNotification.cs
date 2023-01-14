using MS2Project.Application.Bases.DomainEvents;
using MS2Project.Application.Bases.Processing;
using MS2Project.Application.Features.Customers.Commands;
using MS2Project.Domain.CustomerAggregate;
using MS2Project.Domain.CustomerAggregate.Events;

namespace MS2Project.Application.Features.Customers.Notifications;

public class CustomerRegisteredNotification : DomainNotificationBase<CustomerRegisteredEvent>
{
    public CustomerId CustomerId { get; }

    public CustomerRegisteredNotification(CustomerRegisteredEvent domainEvent) : base(domainEvent)
    {
        CustomerId = domainEvent.CustomerId;
    }

    [JsonConstructor]
    public CustomerRegisteredNotification(CustomerId customerId) : base(null)
    {
        CustomerId = customerId;
    }
}

public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public CustomerRegisteredNotificationHandler(
        ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
    {
        // Send welcome e-mail message...

        await _commandsScheduler.EnqueueAsync(new MarkCustomerAsWelcomedCommand(
            Guid.NewGuid(),
            notification.CustomerId));
    }
}
