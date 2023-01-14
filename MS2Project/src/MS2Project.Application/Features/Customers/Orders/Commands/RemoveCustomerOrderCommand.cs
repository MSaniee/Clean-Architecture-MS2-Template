using MS2Project.Domain.CustomerAggregate;
using MS2Project.Domain.CustomerAggregate.Orders;

namespace MS2Project.Application.Features.Customers.Orders.Commands;

public record RemoveCustomerOrderCommand(
    Guid CustomerId,
    Guid OrderId)
    : CommandBase;

public class RemoveCustomerOrderCommandHandler : ICommandHandler<RemoveCustomerOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public RemoveCustomerOrderCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(RemoveCustomerOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId), cancellationToken);

        customer.RemoveOrder(new OrderId(request.OrderId));

        return Unit.Value;
    }
}
