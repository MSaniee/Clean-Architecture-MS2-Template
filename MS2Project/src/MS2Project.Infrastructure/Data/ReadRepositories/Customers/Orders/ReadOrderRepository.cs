using Dapper;
using MS2Project.Application.Dtos.Orders;
using MS2Project.Application.Dtos.Products;
using MS2Project.Application.Interfaces.ReadRepositories.Customers.Orders;

namespace MS2Project.Infrastructure.Data.ReadRepositories.Customers.Orders;

public class ReadOrderRepository : IReadOrderRepository, IScopedDependency
{
    private readonly IDapperDbContext _dapperContext;

    public ReadOrderRepository(IDapperDbContext dapperContext)
    {
        _dapperContext = dapperContext.ThrowIfNull();
    }

    public async Task<List<OrderDto>> GetCustomerOrders(Guid customerId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT " +
                            "[Order].[Id], " +
                            "[Order].[IsRemoved], " +
                            "[Order].[Value], " +
                            "[Order].[Currency] " +
                            "FROM orders.v_Orders AS [Order] " +
                            "WHERE [Order].CustomerId = @CustomerId";

        CommandDefinition commandDefinition = new(
            sql,
            new { CustomerId = customerId },
            cancellationToken: cancellationToken);


        return (await _dapperContext.Connection.QueryAsync<OrderDto>(commandDefinition)).AsList();
    }

    public Task<OrderDetailsDto> GetOrderDetails(Guid orderId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT " +
                           "[Order].[Id], " +
                           "[Order].[IsRemoved], " +
                           "[Order].[Value], " +
                           "[Order].[Currency] " +
                           "FROM orders.v_Orders AS [Order] " +
                           "WHERE [Order].Id = @OrderId";


        CommandDefinition commandDefinition = new(
           sql,
           new { OrderId = orderId },
           cancellationToken: cancellationToken);


        return _dapperContext.Connection.QuerySingleOrDefaultAsync<OrderDetailsDto>(commandDefinition);
    }

    public async Task<List<ProductDto>> GetOrderProducts(Guid orderId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT " +
                           "[Order].[ProductId] AS [Id], " +
                           "[Order].[Quantity], " +
                           "[Order].[Name], " +
                           "[Order].[Value], " +
                           "[Order].[Currency] " +
                           "FROM orders.v_OrderProducts AS [Order] " +
                           "WHERE [Order].OrderId = @OrderId";

        CommandDefinition commandDefinition = new(
           sql,
           new { OrderId = orderId },
           cancellationToken: cancellationToken);

        return (await _dapperContext.Connection.QueryAsync<ProductDto>(commandDefinition)).AsList();

    }
}

