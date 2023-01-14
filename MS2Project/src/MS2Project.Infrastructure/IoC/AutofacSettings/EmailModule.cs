using Autofac;
using MS2Project.Application.Services.Emails;

namespace MS2Project.Infrastructure.IoC.AutofacSettings;

internal class EmailModule : Autofac.Module
{
    private readonly EmailsSettings _emailsSettings;


    internal EmailModule(EmailsSettings emailsSettings)
    {
        _emailsSettings = emailsSettings;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_emailsSettings);
    }
}