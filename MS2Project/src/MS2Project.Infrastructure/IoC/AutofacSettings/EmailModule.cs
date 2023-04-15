using Autofac;
using $ext_safeprojectname$.Application.Services.Emails;

namespace $ext_safeprojectname$.Infrastructure.IoC.AutofacSettings;

public class EmailModule : Autofac.Module
{
    private readonly EmailsSettings _emailsSettings;


    public EmailModule(EmailsSettings emailsSettings)
    {
        _emailsSettings = emailsSettings;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_emailsSettings);
    }
}