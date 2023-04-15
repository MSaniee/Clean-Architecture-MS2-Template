using System.Data;

namespace $ext_safeprojectname$.Infrastructure.Data.SqlServer.Dapper.Context;

public interface IDapperDbContext
{
    IDbConnection Connection { get; }
}

