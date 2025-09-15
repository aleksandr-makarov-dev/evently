using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Evently.Application.Abstractions.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Evently.Infrastructure.Identity;

internal sealed class TokenProvider(IOptions<JwtOptions> options) : ITokenProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public TokenModel Create(IdentityModel identity)
    {
        return new TokenModel(GenerateAccessToken(identity), GenerateRefreshToken());
    }

    private string GenerateAccessToken(IdentityModel model)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, model.UserId),
            new(JwtRegisteredClaimNames.Email, model.Email),
            ..model.Roles.Select(role => new Claim(ClaimTypes.Role, role))
        ];

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
            SigningCredentials = credentials,
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    private static string GenerateRefreshToken()
    {
        byte[] randomNumber = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(randomNumber);
    }
}
