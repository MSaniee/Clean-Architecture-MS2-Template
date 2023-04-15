namespace $ext_safeprojectname$.Application.Services.Emails;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}

