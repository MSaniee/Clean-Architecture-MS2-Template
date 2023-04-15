namespace $ext_safeprojectname$.Domain.Core.Bases.Entities;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}

