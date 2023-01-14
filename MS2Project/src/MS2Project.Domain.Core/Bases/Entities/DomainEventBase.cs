namespace MS2Project.Domain.Core.Bases.Entities;

public class DomainEventBase : IDomainEvent
{
    public DomainEventBase()
    {
        OccurredOn = DateTime.Now;
    }

    public DateTime OccurredOn { get; }
}

