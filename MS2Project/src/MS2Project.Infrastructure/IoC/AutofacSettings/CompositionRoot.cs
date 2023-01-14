using Autofac;

namespace MS2Project.Infrastructure.IoC.AutofacSettings;

public static class CompositionRoot
{
    private static IContainer _container;

    public static void SetContainer(IContainer container)
    {
        _container = container;
    }

    internal static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}
