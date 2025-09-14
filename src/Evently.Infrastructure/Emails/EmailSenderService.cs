using Evently.Application.Abstractions.Emails;
using Evently.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Evently.Infrastructure.Emails;

internal sealed class EmailSenderService(ILogger<EmailSenderService> logger) : IEmailSenderService<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        logger.LogInformation("Sending confirmation email to {Email}", email);
        logger.LogInformation("Confirmation link: {ConfirmationLink}", confirmationLink);

        // TODO: Replace logger with real email sending logic (SMTP, SendGrid, etc.)
        return Task.CompletedTask;
    }


    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}
