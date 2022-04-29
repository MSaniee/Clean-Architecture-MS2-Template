namespace MS2Project.Domain.Core.Bases.Entities;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}

