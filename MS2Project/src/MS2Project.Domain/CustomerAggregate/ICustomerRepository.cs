

namespace $ext_safeprojectname$.Domain.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetByIdAsync(CustomerId id, CancellationToken cancellationToken);
}

