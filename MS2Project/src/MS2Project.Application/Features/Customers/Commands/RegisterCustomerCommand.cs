using $ext_safeprojectname$.Application.Dtos.Customers;
using $ext_safeprojectname$.Domain.Core.Bases.Repositories;
using $ext_safeprojectname$.Domain.CustomerAggregate;

namespace $ext_safeprojectname$.Application.Features.Customers.Commands;

public record RegisterCustomerCommand(
    string Email,
    string Name)
 : CommandBase<SResult<CustomerDto>>;

public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, SResult<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerUniquenessChecker _customerUniquenessChecker;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCustomerCommandHandler(
        ICustomerRepository customerRepository,
        ICustomerUniquenessChecker customerUniquenessChecker,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository.ThrowIfNull();
        _customerUniquenessChecker = customerUniquenessChecker.ThrowIfNull();
        _unitOfWork = unitOfWork.ThrowIfNull();
    }

    public async Task<SResult<CustomerDto>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = Customer.CreateRegistered(request.Email, request.Name, _customerUniquenessChecker);

        await _customerRepository.AddAsync(customer, cancellationToken, false);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomerDto { Id = customer.Id.Value };
    }
}
