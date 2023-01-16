using Autofac.Core;
using MS2Project.Application.Bases.DomainEvents;

namespace MS2Project.Infrastructure.Processing;

public class DomainEventsDispatcher : IDomainEventsDispatcher, IScopedDependency
{
    private readonly IMediator _mediator;
    private readonly ILifetimeScope _scope;
    private readonly ApplicationDbContext _context;

    public DomainEventsDispatcher(
        IMediator mediator,
        ILifetimeScope scope,
        ApplicationDbContext context)
    {
        _mediator = mediator;
        _scope = scope;
        _context = context;
    }

    public async Task DispatchEventsAsync()
    {
        var domainEntities = this._context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
        foreach (var domainEvent in domainEvents)
        {
            Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
            var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
            var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

            if (domainNotification != null)
            {
                domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
            }
        }

        domainEntities
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEventNotifications
            .Select(async (domainEvent) =>
            {
                await _mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}