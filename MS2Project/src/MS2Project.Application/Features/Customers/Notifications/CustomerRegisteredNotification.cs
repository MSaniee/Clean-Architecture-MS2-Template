using $ext_safeprojectname$.Application.Bases.DomainEvents;
using $ext_safeprojectname$.Application.Bases.Processing;
using $ext_safeprojectname$.Application.Features.Customers.Commands;
using $ext_safeprojectname$.Domain.CustomerAggregate;
using $ext_safeprojectname$.Domain.CustomerAggregate.Events;

namespace $ext_safeprojectname$.Application.Features.Customers.Notifications;

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
