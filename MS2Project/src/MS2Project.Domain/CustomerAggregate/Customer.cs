using MS2Project.Domain.Core.Bases;

namespace MS2Project.Domain.CustomerAggregate;

public partial class Customer : BaseEntity<CustomerId>, IAggregateRoot
{

    private string _email;

    private string _name;

    private bool _welcomeEmailWasSent;
}

