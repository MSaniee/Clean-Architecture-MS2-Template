using MS2Project.Domain.Core.Bases;
using MS2Project.Domain.CustomerAggregate.Orders;

namespace MS2Project.Domain.CustomerAggregate;

public partial class Customer : BaseEntity<CustomerId>, IAggregateRoot
{

    private string _email;

    private string _name;

    private readonly List<Order> _orders;

    private bool _welcomeEmailWasSent;

}

