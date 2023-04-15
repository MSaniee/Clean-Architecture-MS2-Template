using Microsoft.EntityFrameworkCore.Query.Internal;

namespace $ext_safeprojectname$.Infrastructure.Data.SqlServer.EfCore.Tools;

public static class DbSetExtensions
{
    public static IQueryable<TEntity> IncludePaths<TEntity>(this IQueryable<TEntity> source,
        params string[] navigationPaths) where TEntity : class
    {
        if (source.Provider is not EntityQueryProvider)
        {
            return source;
        }

        return source.Include(string.Join(".", navigationPaths));
    }
}

