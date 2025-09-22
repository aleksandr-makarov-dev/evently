using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Evently.Application.Abstractions.Clock;
using Evently.Application.Abstractions.Identity;
using Evently.Infrastructure.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Evently.Infrastructure.Identity;

internal sealed class TokenProvider(IOptions<JwtOptions> options, IDateTimeProvider dateTimeProvider) : ITokenProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public TokensModel Create(IdentityModel identity)
    {
        TokenModel accessToken = GenerateAccessToken(identity);
        TokenModel refreshToken = GenerateRefreshToken();

        return new TokensModel(accessToken.Value, accessToken.ExpiresAtUtc, refreshToken.Value,
            refreshToken.ExpiresAtUtc);
    }

    private TokenModel GenerateAccessToken(IdentityModel model)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, model.UserId),
            new(JwtRegisteredClaimNames.Email, model.Email),
            ..model.Roles.Select(role => new Claim(SecurityClaimTypes.Role, role)),
            ..model.Permissions.Select(permission => new Claim(SecurityClaimTypes.Permission, permission))
        ];

        DateTime expiresAtUtc = dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAtUtc,
            SigningCredentials = credentials,
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
        };

        var handler = new JsonWebTokenHandler();

        return new TokenModel(handler.CreateToken(tokenDescriptor), expiresAtUtc);
    }

    private TokenModel GenerateRefreshToken()
    {
        byte[] randomNumber = RandomNumberGenerator.GetBytes(32);

        string token = Convert.ToBase64String(randomNumber);

        DateTime expiresAtUtc = dateTimeProvider.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresInDays);

        return new TokenModel(token, expiresAtUtc);
    }
}

internal record TokenModel(string Value, DateTime ExpiresAtUtc);
