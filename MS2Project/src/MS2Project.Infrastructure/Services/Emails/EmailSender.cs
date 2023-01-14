using MS2Project.Application.Services.Emails;

namespace MS2Project.Infrastructure.Services.Emails;

public class EmailSender : IEmailSender, IScopedDependency
{
    public async Task SendEmailAsync(EmailMessage message)
    {
        // Integration with email service.

        return;
    }
}
