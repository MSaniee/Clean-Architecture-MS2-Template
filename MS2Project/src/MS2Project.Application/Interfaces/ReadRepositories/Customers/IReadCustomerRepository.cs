namespace $ext_safeprojectname$.Application.Interfaces.ReadRepositories.Customers;

public interface IReadCustomerRepository
{
    bool ExistsCustomer(string customerEmail);
}

