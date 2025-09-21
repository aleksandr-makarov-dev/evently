using Evently.Domain.Abstractions;

namespace Evently.Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<Result<string>> RegisterUserAsync(string email, string password,
        CancellationToken cancellationToken = default);

    Task<Result<TokensModel>> LoginUserAsync(string email, string password,
        CancellationToken cancellationToken = default);

    Task<Result<TokensModel>> RefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default);

    Task<Result> LogOutUserAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<Result> ConfirmEmailAsync(Guid userId, string code, CancellationToken cancellationToken = default);

    Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email,
        CancellationToken cancellationToken = default);
}
