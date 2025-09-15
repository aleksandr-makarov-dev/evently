namespace Evently.Application.Abstractions.Emails;

public interface IEmailSenderService<in TUser>
{
    Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink);

    Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink);

    Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode);

    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
