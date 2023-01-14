using System.Data;

namespace MS2Project.Infrastructure.Data.SqlServer.Dapper.Context;

public interface IDapperDbContext
{
    IDbConnection Connection { get; }
}

