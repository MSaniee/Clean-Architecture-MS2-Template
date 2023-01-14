using MS2Project.Application.Features.Customers.Orders.Commands;

namespace MS2Project.Infrastructure.Processing;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(PlaceCustomerOrderCommand).Assembly;
}
