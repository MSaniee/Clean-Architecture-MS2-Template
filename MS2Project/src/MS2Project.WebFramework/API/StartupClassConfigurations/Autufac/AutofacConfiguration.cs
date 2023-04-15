using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using $ext_safeprojectname$.Application.Services.Emails;
using $ext_safeprojectname$.Infrastructure.IoC.AutofacSettings;
using Serilog;
using Serilog.Formatting.Compact;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations.Autufac;

public static class AutofacConfiguration
{
    public static void AddAutofacService(this IHostBuilder hostbuilder, IConfiguration configuration)
    {
        hostbuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        // Register services directly with Autofac here. Don't
        // call builder.Populate(), that happens in AutofacServiceProviderFactory.
        hostbuilder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterModule(new LifeTimeModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new LoggingModule(ConfigureLogger()));
            containerBuilder.RegisterModule(new EmailModule(configuration.GetSection("EmailsSettings").Get<EmailsSettings>()));
            containerBuilder.RegisterModule(new ProcessingModule());
        });
    }

    private static ILogger ConfigureLogger()
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
            .CreateLogger();
    }
}

