using $ext_safeprojectname$.Application.Bases.Commands;
using $ext_safeprojectname$.Application.Bases.DomainEvents;
using $ext_safeprojectname$.Application.Bases.Processing;
using $ext_safeprojectname$.Application.Features.Customers.Notifications;
using $ext_safeprojectname$.Infrastructure.Processing;

namespace $ext_safeprojectname$.Infrastructure.IoC.AutofacSettings;

public class ProcessingModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterGenericDecorator(
        //    typeof(DomainEventsDispatcherNotificationHandlerDecorator<>),
        //    typeof(INotificationHandler<>));


        builder.RegisterType<CommandsScheduler>()
            .As<ICommandsScheduler>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(CustomerRegisteredNotification).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IDomainEventNotification<>)).InstancePerDependency();
    }
}
