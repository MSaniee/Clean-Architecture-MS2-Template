using Serilog;

namespace MS2Project.Infrastructure.IoC.AutofacSettings;

public class LoggingModule : Autofac.Module
{
    private readonly ILogger _logger;

    public LoggingModule(ILogger logger)
    {
        _logger = logger;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_logger)
            .As<ILogger>()
            .SingleInstance();
    }
}
