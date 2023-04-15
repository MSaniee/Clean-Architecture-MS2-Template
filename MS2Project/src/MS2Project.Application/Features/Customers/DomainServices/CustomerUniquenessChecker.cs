using $ext_safeprojectname$.Application.Interfaces.ReadRepositories.Customers;
using $ext_safeprojectname$.Domain.CustomerAggregate;

namespace $ext_safeprojectname$.Application.Features.Customers.DomainServices;

public class CustomerUniquenessChecker : ICustomerUniquenessChecker, IScopedDependency
{
    private readonly IReadCustomerRepository _customerRepo;

    public CustomerUniquenessChecker(IReadCustomerRepository customerRepo)
    {
        _customerRepo = customerRepo.ThrowIfNull();
    }

    //I awaiting for get answer why dont use asyn in main sample
    public bool IsUnique(string customerEmail)
        => _customerRepo.ExistsCustomer(customerEmail);
}
