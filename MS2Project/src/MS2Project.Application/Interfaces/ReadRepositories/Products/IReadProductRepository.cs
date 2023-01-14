using MS2Project.Domain.ProductAggregate;

namespace MS2Project.Application.Interfaces.ReadRepositories.Products;

public interface IReadProductRepository
{
    Task<List<ProductPriceData>> GetAllProductPrices(CancellationToken cancellationToken);
}

