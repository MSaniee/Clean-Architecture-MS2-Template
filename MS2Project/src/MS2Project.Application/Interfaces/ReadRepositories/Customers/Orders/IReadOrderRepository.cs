using MS2Project.Application.Dtos.Orders;
using MS2Project.Application.Dtos.Products;

namespace MS2Project.Application.Interfaces.ReadRepositories.Customers.Orders;

public interface IReadOrderRepository
{
    Task<List<OrderDto>> GetCustomerOrders(Guid customerId, CancellationToken cancellationToken);
    Task<OrderDetailsDto> GetOrderDetails(Guid orderId, CancellationToken cancellationToken);
    Task<List<ProductDto>> GetOrderProducts(Guid orderId, CancellationToken cancellationToken);
}

