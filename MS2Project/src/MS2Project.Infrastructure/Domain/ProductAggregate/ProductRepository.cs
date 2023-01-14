using MS2Project.Domain.Core.DILifeTimesType;
using MS2Project.Domain.ProductAggregate;
using MS2Project.Infrastructure.Data.Bases;

namespace MS2Project.Infrastructure.Domain.ProductAggregate;

public class ProductRepository : Repository<Product>, IProductRepository, IScopedDependency
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<List<Product>> GetByIdsAsync(List<ProductId> ids)
    {
        return Table.IncludePaths("_prices")
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();
    }

    public Task<List<Product>> GetAllAsync()
    {
        return Table.IncludePaths("_prices").ToListAsync();
    }
}
