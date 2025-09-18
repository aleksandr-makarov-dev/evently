namespace Evently.Application.Abstractions.Emails;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
