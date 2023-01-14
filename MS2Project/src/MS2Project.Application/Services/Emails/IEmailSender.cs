namespace MS2Project.Application.Services.Emails;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}

