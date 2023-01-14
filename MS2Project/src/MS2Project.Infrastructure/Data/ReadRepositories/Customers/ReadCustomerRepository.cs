using Dapper;
using MS2Project.Application.Interfaces.ReadRepositories.Customers;
using MS2Project.Infrastructure.Data.SqlServer.Dapper.Context;

namespace MS2Project.Infrastructure.Data.ReadRepositories.Customers;

public class ReadCustomerRepository : IReadCustomerRepository, IScopedDependency
{
    private readonly IDapperDbContext _dapperContext;

    public ReadCustomerRepository(IDapperDbContext dapperContext)
    {
        _dapperContext = dapperContext.ThrowIfNull();
    }

    public  bool ExistsCustomer(string customerEmail)
    {
        const string sql = "SELECT TOP 1 1" +
                           "FROM [orders].[Customers] AS [Customer] " +
                           "WHERE [Customer].[Email] = @Email";

        CommandDefinition commandDefinition = new(
            sql,
            new { Email = customerEmail});

        var customersNumber =  _dapperContext.Connection
                 .QuerySingleOrDefault<int?>(commandDefinition);

        return !customersNumber.HasValue;
    }
}

