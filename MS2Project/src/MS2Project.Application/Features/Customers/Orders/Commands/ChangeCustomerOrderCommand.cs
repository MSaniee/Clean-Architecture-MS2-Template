using MS2Project.Application.Dtos.Products;
using MS2Project.Application.Interfaces.ReadRepositories.Products;
using MS2Project.Domain.CustomerAggregate;
using MS2Project.Domain.CustomerAggregate.Orders;
using MS2Project.Domain.ProductAggregate;
using MS2Project.Domain.SharedKernel.ForeignExchange;

namespace MS2Project.Application.Features.Customers.Orders.Commands;

public record ChangeCustomerOrderCommand(
    Guid CustomerId,
    Guid OrderId,
    string Currency,
    List<ProductDto> Products)
    : CommandBase<Unit>;


internal sealed class ChangeCustomerOrderCommandHandler : ICommandHandler<ChangeCustomerOrderCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IForeignExchange _foreignExchange;

    private readonly IReadProductRepository _readProductRepo;

    internal ChangeCustomerOrderCommandHandler(
        ICustomerRepository customerRepository,
        IForeignExchange foreignExchange,
        IReadProductRepository readProductRepo)
    {
        _customerRepository = customerRepository.ThrowIfNull();
        _foreignExchange = foreignExchange.ThrowIfNull();
        _readProductRepo = readProductRepo.ThrowIfNull();
    }

    public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId), cancellationToken);

        var orderId = new OrderId(request.OrderId);

        var conversionRates = _foreignExchange.GetConversionRates();
        var orderProducts = request
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                .ToList();

        var allProductPrices = await _readProductRepo.GetAllProductPrices(cancellationToken);

        customer.ChangeOrder(
            orderId,
            allProductPrices,
            orderProducts,
            conversionRates,
            request.Currency);

        return Unit.Value;
    }
}



