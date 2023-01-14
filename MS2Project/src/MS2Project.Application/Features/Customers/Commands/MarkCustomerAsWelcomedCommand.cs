using MS2Project.Domain.CustomerAggregate;

namespace MS2Project.Application.Features.Customers.Commands;

public record MarkCustomerAsWelcomedCommand : InternalCommandBase<Unit>
{
    [JsonConstructor]
    public MarkCustomerAsWelcomedCommand(Guid id, CustomerId customerId) : base(id)
    {
        CustomerId = customerId;
    }

    public CustomerId CustomerId { get; }
}

public class MarkCustomerAsWelcomedCommandHandler : ICommandHandler<MarkCustomerAsWelcomedCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public MarkCustomerAsWelcomedCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(MarkCustomerAsWelcomedCommand command, CancellationToken cancellationToken)
    {
        Customer customer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);

        customer.MarkAsWelcomedByEmail();

        return Unit.Value;
    }
}