using Autofac.Core.Activators.Reflection;
using System.Collections.Concurrent;

namespace MS2Project.Infrastructure.Processing;

internal class AllConstructorFinder : IConstructorFinder
{
    private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache = new();

    public ConstructorInfo[] FindConstructors(Type targetType)
    {
        var result = Cache.GetOrAdd(targetType,
            t => t.GetTypeInfo().DeclaredConstructors.ToArray());

        return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType);
    }
}