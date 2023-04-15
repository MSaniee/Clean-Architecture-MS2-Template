namespace $ext_safeprojectname$.Domain.CustomerAggregate.Rules;

public class CustomerEmailMustBeUniqueRule : IBusinessRule
{
    private readonly ICustomerUniquenessChecker _customerUniquenessChecker;

    private readonly string _email;

    public CustomerEmailMustBeUniqueRule(
        ICustomerUniquenessChecker customerUniquenessChecker,
        string email)
    {
        _customerUniquenessChecker = customerUniquenessChecker;
        _email = email;
    }

    public bool IsBroken() => !_customerUniquenessChecker.IsUnique(_email);

    public string Message => Memos.CustomerWithEmailAlreadyExists;
}


