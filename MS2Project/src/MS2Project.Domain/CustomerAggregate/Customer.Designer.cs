using MS2Project.Domain.CustomerAggregate.Events;
using MS2Project.Domain.CustomerAggregate.Orders;
using MS2Project.Domain.CustomerAggregate.Orders.Events;
using MS2Project.Domain.CustomerAggregate.Rules;
using MS2Project.Domain.ProductAggregate;
using MS2Project.Domain.SharedKernel.ForeignExchange;

namespace MS2Project.Domain.CustomerAggregate;

public partial class Customer
{
    private Customer()
    {
        _orders = new List<Order>();
    }


    private Customer(string email, string name)
    {
        _email = email;
        _name = name;
        _welcomeEmailWasSent = false;
        _orders = new List<Order>();

        AddDomainEvent(new CustomerRegisteredEvent(Id));
    }

    public static Customer CreateRegistered(
            string email,
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
    {
        CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

        return new Customer(email, name);
    }

    public OrderId PlaceOrder(
            List<OrderProductData> orderProductsData,
            List<ProductPriceData> allProductPrices,
            string currency,
            List<ConversionRate> conversionRates)
    {
        CheckRule(new CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule(_orders));
        CheckRule(new OrderMustHaveAtLeastOneProductRule(orderProductsData));

        Order order = Order.CreateNew(orderProductsData, allProductPrices, currency, conversionRates);

        _orders.Add(order);

        AddDomainEvent(new OrderPlacedEvent(order.Id, Id, order.GetValue()));

        return order.Id;
    }

    public void ChangeOrder(
        OrderId orderId,
        List<ProductPriceData> existingProducts,
        List<OrderProductData> newOrderProductsData,
        List<ConversionRate> conversionRates,
        string currency)
    {
        CheckRule(new OrderMustHaveAtLeastOneProductRule(newOrderProductsData));

        Order order = _orders.Single(x => x.Id == orderId);
        order.Change(existingProducts, newOrderProductsData, conversionRates, currency);

        AddDomainEvent(new OrderChangedEvent(orderId));
    }

    public void RemoveOrder(OrderId orderId)
    {
        Order order = _orders.Single(x => x.Id == orderId);
        order.Remove();

        AddDomainEvent(new OrderRemovedEvent(orderId));
    }

    public void MarkAsWelcomedByEmail()
    {
        _welcomeEmailWasSent = true;
    }
}

public class CustomerId : TypedIdValueBase
{
    //private CustomerId() { }

    public CustomerId(Guid value) : base(value)
    {
    }
}

