namespace MS2Project.Application.Bases.DomainEvents;

public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
{
    TEventType DomainEvent { get; }
}

public interface IDomainEventNotification : INotification
{
    Guid Id { get; }
}