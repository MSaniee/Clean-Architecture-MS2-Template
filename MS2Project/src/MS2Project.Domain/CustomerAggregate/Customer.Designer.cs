using $ext_safeprojectname$.Domain.CustomerAggregate.Events;
using $ext_safeprojectname$.Domain.CustomerAggregate.Rules;

namespace $ext_safeprojectname$.Domain.CustomerAggregate;

public partial class Customer
{
    private Customer()
    {
    }


    private Customer(string email, string name)
    {
        Id = new(Guid.NewGuid());
        _email = email;
        _name = name;
        _welcomeEmailWasSent = false;

        AddDomainEvent(new CustomerRegisteredEvent(Id));
    }

    public static Customer CreateRegistered(
            string email,
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
    {
        CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

        return new Customer(email, name);
    }

    public void MarkAsWelcomedByEmail()
    {
        _welcomeEmailWasSent = true;
    }
}

public class CustomerId : TypedIdValueBase
{
    //private CustomerId() { }

    public CustomerId(Guid value) : base(value)
    {
    }
}

