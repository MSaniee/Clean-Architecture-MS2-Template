using $ext_safeprojectname$.Application.Dtos.Customers;
using $ext_safeprojectname$.Application.Features.Customers.Commands;


namespace $ext_safeprojectname$.API.Areas.Customers;

[Area("Customers")]
[ApiVersion("1")]
[AllowAnonymous]
//[Authorize(Roles = "User")]
[ApiExplorerSettings(GroupName = "Customers")]
public class CustomersController : BaseController
{
    private readonly IMediator _mediator;

    public CustomersController(
        IMediator mediator)
    {
        _mediator = mediator.ThrowIfNull();
    }

    /// <summary>
    /// ثبت مشتری
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ApiResult<CustomerDto>> RegisterCustomer(RegisterCustomerDto dto, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
               new RegisterCustomerCommand(dto.Email, dto.Name),
               cancellationToken);
    }
}

