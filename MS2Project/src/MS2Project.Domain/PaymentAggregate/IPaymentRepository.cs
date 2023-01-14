namespace MS2Project.Domain.PaymentAggregate;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment> GetByIdAsync(PaymentId id);
}