using MS2Project.Application.Bases.Queries;
using MS2Project.Application.Dtos.Orders;
using MS2Project.Application.Interfaces.ReadRepositories.Customers.Orders;

namespace MS2Project.Application.Features.Customers.Orders.Queries;

public record GetCustomerOrdersQuery(
    Guid CustomerId)
: IQuery<List<OrderDto>>;


internal sealed class GetCustomerOrdersQueryHandler : IQueryHandler<GetCustomerOrdersQuery, List<OrderDto>>
{
    private readonly IReadOrderRepository _orderRepo;

    internal GetCustomerOrdersQueryHandler(IReadOrderRepository orderRepo)
    {
        _orderRepo = orderRepo.ThrowIfNull();
    }

    public Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        return _orderRepo.GetCustomerOrders(request.CustomerId, cancellationToken);
    }
}

