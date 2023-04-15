using Autofac;

namespace $ext_safeprojectname$.Infrastructure.IoC.AutofacSettings;

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
