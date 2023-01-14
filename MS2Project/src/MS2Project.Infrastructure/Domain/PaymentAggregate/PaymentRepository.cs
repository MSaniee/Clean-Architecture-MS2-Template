using MS2Project.Domain.PaymentAggregate;

namespace MS2Project.Infrastructure.Domain.PaymentAggregate;

public class PaymentRepository : Repository<Payment>, IPaymentRepository, IScopedDependency
{
    public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Payment> GetByIdAsync(PaymentId id)
    {
        return await Table.SingleAsync(x => x.Id == id);
    }

}

