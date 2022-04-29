using MS2Project.Domain.Core.Bases.Entities;
using MS2Project.Domain.Core.Exceptions;

namespace MS2Project.Domain.Core.Bases;

public abstract class BaseEntity<TKey> : IEntity
{
    public TKey Id { get; set; }

    private List<IDomainEvent> _domainEvents;

    /// <summary>
    /// Domain events occurred.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        this._domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clear domain events.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}

public abstract class BaseEntity : BaseEntity<int>
{
}
