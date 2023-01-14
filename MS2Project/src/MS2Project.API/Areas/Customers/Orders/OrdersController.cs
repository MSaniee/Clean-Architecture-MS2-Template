using MS2Project.Application.Dtos.Orders;
using MS2Project.Application.Features.Customers.Orders.Commands;
using MS2Project.Application.Features.Customers.Orders.Queries;

namespace MS2Project.API.Areas.Customers.Orders;

[Area("Customers")]
[ApiVersion("1")]
[AllowAnonymous]
//[Authorize(Roles = "User")]
[ApiExplorerSettings(GroupName = "Customers - Orders")]
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;

    public OrdersController(
        IMediator mediator)
    {
        _mediator = mediator.ThrowIfNull();
    }

    /// <summary>
    /// دریافت سفارش های یک مشتری
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ApiResult<List<OrderDto>>> GetCustomerOrders(Guid customerId, CancellationToken cancellationToken)

        => await _mediator.Send(new GetCustomerOrdersQuery(customerId), cancellationToken);


    /// <summary>
    /// دریافت جزئیات سفارش یک مشتری
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ApiResult<OrderDetailsDto>> GetCustomerOrderDetails(Guid orderId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetCustomerOrderDetailsQuery(orderId), cancellationToken);
    }

    /// <summary>
    /// افزودن یک سفارش
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ApiResult> AddCustomerOrder(
            [FromRoute] Guid customerId,
            [FromBody] CustomerOrderDto dto)
    {
        //I think we can get CustomerId from Jwt token

        await _mediator.Send(new PlaceCustomerOrderCommand(customerId, dto.Products, dto.Currency));

        return Ok();
    }

    /// <summary>
    /// ویرایش یک سفارش
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="customerId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ApiResult> ChangeCustomerOrder(
        CustomerOrderDto dto,
        Guid customerId,
        Guid orderId)
    {
        await _mediator.Send(new ChangeCustomerOrderCommand(customerId, orderId, dto.Currency, dto.Products));

        return Ok();
    }

    /// <summary>
    /// حذف یک سفارش
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ApiResult> RemoveCustomerOrder(
            Guid customerId,
            Guid orderId)
    {
        await _mediator.Send(new RemoveCustomerOrderCommand(customerId, orderId));

        return Ok();
    }
}
