namespace Evently.Infrastructure.Identity;

internal sealed class JwtOptions
{
    public string Issuer { get; init; }

    public string Audience { get; init; }

    public string Key { get; init; }

    public int ExpiresInMinutes { get; init; }

    public int RefreshTokenExpiresInDays { get; init; }
}
