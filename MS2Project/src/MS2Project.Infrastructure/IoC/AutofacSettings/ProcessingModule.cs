using MS2Project.Application.Bases.Commands;
using MS2Project.Application.Bases.DomainEvents;
using MS2Project.Application.Bases.Processing;
using MS2Project.Application.Features.Payments.Notifications;
using MS2Project.Infrastructure.Logging;
using MS2Project.Infrastructure.Processing;
using MS2Project.Infrastructure.Processing.InternalCommands;

namespace MS2Project.Infrastructure.IoC.AutofacSettings;

public class ProcessingModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(PaymentCreatedNotification).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IDomainEventNotification<>)).InstancePerDependency();

        builder.RegisterGenericDecorator(
            typeof(DomainEventsDispatcherNotificationHandlerDecorator<>),
            typeof(INotificationHandler<>));

        builder.RegisterGenericDecorator(
            typeof(UnitOfWorkCommandHandlerDecorator<>),
            typeof(ICommandHandler<>));

        builder.RegisterGenericDecorator(
            typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
            typeof(ICommandHandler<,>));

        builder.RegisterType<CommandsDispatcher>()
            .As<ICommandsDispatcher>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CommandsScheduler>()
            .As<ICommandsScheduler>()
            .InstancePerLifetimeScope();

        builder.RegisterGenericDecorator(
            typeof(LoggingCommandHandlerDecorator<>),
            typeof(ICommandHandler<>));

        builder.RegisterGenericDecorator(
            typeof(LoggingCommandHandlerWithResultDecorator<,>),
            typeof(ICommandHandler<,>));
    }
}
