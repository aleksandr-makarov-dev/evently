namespace Evently.Infrastructure.Emails;

internal sealed class SmtpOptions
{
    public string Host { get; init; } = string.Empty;

    public int Port { get; init; }

    public bool UseSsl { get; init; }

    public string UserName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string FromAddress { get; init; } = string.Empty;
}
