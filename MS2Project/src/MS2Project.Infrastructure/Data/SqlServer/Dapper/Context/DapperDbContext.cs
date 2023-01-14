using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace MS2Project.Infrastructure.Data.SqlServer.Dapper.Context;

public class DapperDbContext : IScopedDependency, IDapperDbContext
{
    private readonly IConfiguration _config;
    private const string _connectionstring = "SqlServer";

    public DapperDbContext(IConfiguration config)
    {
        _config = config.ThrowIfNull();
    }

    public IDbConnection Connection => new SqlConnection(_config.GetConnectionString(_connectionstring));
}
