namespace $ext_safeprojectname$.Domain.CustomerAggregate;

public interface ICustomerUniquenessChecker
{
    bool IsUnique(string customerEmail);
}

