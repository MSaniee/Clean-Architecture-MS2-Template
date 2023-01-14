using MS2Project.Infrastructure.Processing;

namespace MS2Project.Infrastructure.Data.Bases;

public class UnitOfWork : IUnitOfWork, IScopedDependency
{
    private readonly ApplicationDbContext _context;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public UnitOfWork(
        ApplicationDbContext context,
        IDomainEventsDispatcher domainEventsDispatcher)
    {
        _context = context;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await _domainEventsDispatcher.DispatchEventsAsync();
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
