namespace Evently.Application.Abstractions.Authentication;

public record TokensModel(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc
);
