using $ext_safeprojectname$.Domain.Core.Bases;

namespace $ext_safeprojectname$.Domain.CustomerAggregate;

public partial class Customer : BaseEntity<CustomerId>, IAggregateRoot
{

    private string _email;

    private string _name;

    private bool _welcomeEmailWasSent;
}

