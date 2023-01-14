

namespace MS2Project.Domain.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetByIdAsync(CustomerId id, CancellationToken cancellationToken);
}

