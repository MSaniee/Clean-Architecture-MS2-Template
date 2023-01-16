using MS2Project.Application.Bases.Commands;
using MS2Project.Application.Bases.DomainEvents;
using MS2Project.Application.Bases.Processing;
using MS2Project.Application.Features.Customers.Notifications;
using MS2Project.Infrastructure.Processing;

namespace MS2Project.Infrastructure.IoC.AutofacSettings;

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
