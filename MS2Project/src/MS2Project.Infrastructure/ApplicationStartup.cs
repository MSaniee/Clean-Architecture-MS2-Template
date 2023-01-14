using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using MS2Project.Application.Configurations;
using MS2Project.Application.Services.Emails;
using MS2Project.Infrastructure.Caching;
using MS2Project.Infrastructure.IoC.AutofacSettings;
using MS2Project.Infrastructure.Processing.InternalCommands;
using MS2Project.Infrastructure.Processing.Outbox;
using MS2Project.Infrastructure.Quartz;
using Quartz;
using Quartz.Impl;
using Serilog;
using TriggerBuilder = Quartz.TriggerBuilder;

namespace MS2Project.Infrastructure;

public class ApplicationStartup
{
    public static IServiceProvider Initialize(
        IServiceCollection services,
        ICacheStore cacheStore,
        EmailsSettings emailsSettings,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor,
        bool runQuartz = true)
    {
        if (runQuartz)
        {
            StartQuartz(emailsSettings, logger, executionContextAccessor);
        }

        services.AddSingleton(cacheStore);

        var serviceProvider = CreateAutofacServiceProvider(
            services,
            emailsSettings,
            logger,
            executionContextAccessor);

        return serviceProvider;
    }

    private static IServiceProvider CreateAutofacServiceProvider(
        IServiceCollection services,
        EmailsSettings emailsSettings,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor)
    {
        var container = new ContainerBuilder();

        container.Populate(services);

        container.RegisterModule(new LifeTimeModule());
        container.RegisterModule(new LoggingModule(logger));
        container.RegisterModule(new MediatorModule());
        container.RegisterModule(new EmailModule(emailsSettings));


        container.RegisterModule(new ProcessingModule());

        container.RegisterInstance(executionContextAccessor);

        var buildContainer = container.Build();

        ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

        var serviceProvider = new AutofacServiceProvider(buildContainer);

        CompositionRoot.SetContainer(buildContainer);

        return serviceProvider;
    }

    private static void StartQuartz(
        EmailsSettings emailsSettings,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor)
    {
        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

        var container = new ContainerBuilder();

        container.RegisterModule(new LoggingModule(logger));
        container.RegisterModule(new QuartzModule());
        container.RegisterModule(new MediatorModule());
        container.RegisterModule(new LifeTimeModule());
        container.RegisterModule(new EmailModule(emailsSettings));
        container.RegisterModule(new ProcessingModule());

        container.RegisterInstance(executionContextAccessor);
        //container.Register(c =>
        //{
        //    var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrdersContext>();
        //    dbContextOptionsBuilder.UseSqlServer(connectionString);

        //    dbContextOptionsBuilder
        //        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

        //    return new OrdersContext(dbContextOptionsBuilder.Options);
        //}).AsSelf().InstancePerLifetimeScope();

        scheduler.JobFactory = new JobFactory(container.Build());

        scheduler.Start().GetAwaiter().GetResult();

        var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
        var trigger =
            TriggerBuilder
                .Create()
                .StartNow()
                .WithCronSchedule("0/15 * * ? * *")
                .Build();

        scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();

        var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
        var triggerCommandsProcessing =
            TriggerBuilder
                .Create()
                .StartNow()
                .WithCronSchedule("0/15 * * ? * *")
                .Build();
        scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
    }
}
