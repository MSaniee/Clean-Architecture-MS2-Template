using MS2Project.Application.Dtos.Products;
using MS2Project.Application.Interfaces.ReadRepositories.Products;
using MS2Project.Domain.CustomerAggregate;
using MS2Project.Domain.CustomerAggregate.Orders;
using MS2Project.Domain.ProductAggregate;
using MS2Project.Domain.SharedKernel.ForeignExchange;

namespace MS2Project.Application.Features.Customers.Orders.Commands;

public record PlaceCustomerOrderCommand(
    Guid CustomerId,
    List<ProductDto> Products,
    string Currency)
    : CommandBase<SResult<Guid>>;

public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, SResult<Guid>>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IReadProductRepository _productRepo;

    private readonly IForeignExchange _foreignExchange;

    public PlaceCustomerOrderCommandHandler(
        ICustomerRepository customerRepository,
        IForeignExchange foreignExchange,
        IReadProductRepository productRepo)
    {
        _customerRepository = customerRepository.ThrowIfNull();
        _foreignExchange = foreignExchange.ThrowIfNull();
        _productRepo = productRepo.ThrowIfNull();
    }

    public async Task<SResult<Guid>> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
    {
        Customer customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId), cancellationToken);

        var allProductPrices = await _productRepo.GetAllProductPrices(cancellationToken);

        var conversionRates = _foreignExchange.GetConversionRates();

        var orderProductsData = command
            .Products
            .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
            .ToList();

        var orderId = customer.PlaceOrder(
            orderProductsData,
            allProductPrices,
            command.Currency,
            conversionRates);

        return orderId.Value;
    }
}

