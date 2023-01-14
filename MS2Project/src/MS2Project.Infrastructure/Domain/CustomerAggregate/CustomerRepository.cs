using MS2Project.Domain.CustomerAggregate;

namespace MS2Project.Infrastructure.Domain.CustomerAggregate;

public class CustomerRepository : Repository<Customer>, ICustomerRepository, IScopedDependency
{
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Customer> GetByIdAsync(CustomerId id, CancellationToken cancellationToken)
    {
        return await Table.IncludePaths(
                    CustomerEntityTypeConfiguration.OrdersList,
                    "OrderProducts")
            .SingleAsync(x => x.Id == id, cancellationToken);
    }
}

