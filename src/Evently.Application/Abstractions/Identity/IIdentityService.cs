using Evently.Domain.Abstractions;

namespace Evently.Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<Result<string>> RegisterUserAsync(string email, string password,
        CancellationToken cancellationToken = default);

    Task<Result<TokenModel>> LoginUserAsync(string email, string password,
        CancellationToken cancellationToken = default);

    Task<Result<TokenModel>> RefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default);
}
