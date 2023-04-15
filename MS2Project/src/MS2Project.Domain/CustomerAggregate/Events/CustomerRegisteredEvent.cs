namespace $ext_safeprojectname$.Domain.CustomerAggregate.Events;

public class CustomerRegisteredEvent : DomainEventBase
{
    public CustomerId CustomerId { get; }


    public CustomerRegisteredEvent(CustomerId customerId)
    {
        CustomerId = customerId;
    }
}

