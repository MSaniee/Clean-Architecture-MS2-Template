using MS2Project.Application.Dtos.Orders;
using MS2Project.Application.Interfaces.ReadRepositories.Customers.Orders;

namespace MS2Project.Application.Features.Customers.Orders.Queries;

public record GetCustomerOrderDetailsQuery(
    Guid OrderId)
    : IQuery<OrderDetailsDto>;

internal sealed class GetCustomerOrderDetailsQueryHandler : IQueryHandler<GetCustomerOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IReadOrderRepository _orderRepo;

    internal GetCustomerOrderDetailsQueryHandler(IReadOrderRepository orderRepo)
    {
        _orderRepo = orderRepo.ThrowIfNull();
    }

    public async Task<OrderDetailsDto> Handle(GetCustomerOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        OrderDetailsDto order = await _orderRepo.GetOrderDetails(request.OrderId, cancellationToken);

        order.Products = await _orderRepo.GetOrderProducts(request.OrderId, cancellationToken);

        return order;
    }
}

