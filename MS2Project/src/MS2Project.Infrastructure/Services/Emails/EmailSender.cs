using $ext_safeprojectname$.Application.Services.Emails;

namespace $ext_safeprojectname$.Infrastructure.Services.Emails;

public class EmailSender : IEmailSender, IScopedDependency
{
    public async Task SendEmailAsync(EmailMessage message)
    {
        // Integration with email service.

        return;
    }
}
