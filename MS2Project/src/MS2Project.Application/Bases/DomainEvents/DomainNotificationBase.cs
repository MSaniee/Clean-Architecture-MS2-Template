namespace MS2Project.Application.Bases.DomainEvents;

public class DomainNotificationBase<T> : IDomainEventNotification<T> where T : IDomainEvent
{
    [JsonIgnore]
    public T DomainEvent { get; }

    public Guid Id { get; }

    public DomainNotificationBase(T domainEvent)
    {
        Id = Guid.NewGuid();
        DomainEvent = domainEvent;
    }
}
