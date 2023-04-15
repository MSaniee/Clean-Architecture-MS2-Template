using Dapper;
using $ext_safeprojectname$.Application.Interfaces.ReadRepositories.Customers;
using $ext_safeprojectname$.Infrastructure.Data.SqlServer.Dapper.Context;

namespace $ext_safeprojectname$.Infrastructure.Data.ReadRepositories.Customers;

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
                           "FROM [Customers]" +
                           "WHERE [Customers].[Email] = @Email";

        CommandDefinition commandDefinition = new(
            sql,
            new { Email = customerEmail});

        var customersNumber =  _dapperContext.Connection
                 .QuerySingleOrDefault<int?>(commandDefinition);

        return !customersNumber.HasValue;
    }
}

