namespace Evently.Application.Abstractions.Identity;

public record TokensModel(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc
);
