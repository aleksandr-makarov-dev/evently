using Evently.Application.Abstractions.Emails;
using Evently.Domain.Users;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Evently.Infrastructure.Emails;

internal sealed class EmailSenderService(ILogger<EmailSenderService> logger, IOptions<SmtpOptions> smtpOptions)
{
    private readonly SmtpOptions _smtpOptions = smtpOptions.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();

        message.From.Add(MailboxAddress.Parse(_smtpOptions.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.UseSsl);
        await smtpClient.AuthenticateAsync(_smtpOptions.UserName, _smtpOptions.Password);

        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }
}
