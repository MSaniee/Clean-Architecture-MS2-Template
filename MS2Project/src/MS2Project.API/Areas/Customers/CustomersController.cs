using MS2Project.Application.Dtos.Customers;
using MS2Project.Application.Features.Customers.Commands;


namespace MS2Project.API.Areas.Customers;

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

    /// <summary>
    /// برای تست
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ApiResult> TestWebservice(CancellationToken cancellationToken)
    {
        return Ok();
    }
}

