namespace MS2Project.Application.Interfaces.ReadRepositories.Customers;

public interface IReadCustomerRepository
{
    bool ExistsCustomer(string customerEmail);
}

