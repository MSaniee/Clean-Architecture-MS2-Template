using MS2Project.Application.Interfaces.ReadRepositories.Customers;
using MS2Project.Domain.CustomerAggregate;

namespace MS2Project.Application.Features.Customers.DomainServices;

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
