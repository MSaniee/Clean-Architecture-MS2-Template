using Dapper;
using MS2Project.Application.Interfaces.ReadRepositories.Products;
using MS2Project.Domain.ProductAggregate;
using MS2Project.Domain.SharedKernel.Money;

namespace MS2Project.Infrastructure.Data.ReadRepositories.Products;

public class ReadProductRepository : IReadProductRepository, IScopedDependency
{
    private readonly IDapperDbContext _dapperContext;

    public ReadProductRepository(IDapperDbContext dapperContext)
    {
        _dapperContext = dapperContext.ThrowIfNull();
    }

    public async Task<List<ProductPriceData>> GetAllProductPrices(CancellationToken cancellationToken)
    {
        const string sql = "SELECT " +
                           $"[ProductPrice].ProductId AS [{nameof(ProductPriceResponse.ProductId)}], " +
                           $"[ProductPrice].Value AS [{nameof(ProductPriceResponse.Value)}], " +
                           $"[ProductPrice].Currency AS [{nameof(ProductPriceResponse.Currency)}] " +
                           "FROM orders.v_ProductPrices AS [ProductPrice]";

        CommandDefinition commandDefinition = new(
            sql,
            cancellationToken: cancellationToken);

        var productPrices = await _dapperContext.Connection
                 .QueryAsync<ProductPriceResponse>(commandDefinition);

        return productPrices.AsList()
            .Select(x => new ProductPriceData(
                new ProductId(x.ProductId),
                MoneyValue.Of(x.Value, x.Currency)))
            .ToList();
    }

    private sealed class ProductPriceResponse
    {
        public Guid ProductId { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }
    }
}

