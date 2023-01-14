namespace MS2Project.Domain.CustomerAggregate;

public interface ICustomerUniquenessChecker
{
    bool IsUnique(string customerEmail);
}

