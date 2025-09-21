using Evently.Application.Abstractions.Identity;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(
    IIdentityService identityService,
    ILogger<LoginUserCommandHandler> logger
) : ICommandHandler<LoginUserCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<TokensModel> tokens = await identityService.LoginUserAsync(
            request.Email,
            request.Password,
            cancellationToken
        );

        if (tokens.IsFailure)
        {
            logger.LogWarning("Login attempt failed for email {Email}. Error: {Error}",
                request.Email,
                tokens.Error);

            return Result.Failure<TokenResponse>(tokens.Error);
        }

        return Result.Success(new TokenResponse(
            tokens.Value.AccessToken,
            tokens.Value.AccessTokenExpiresAtUtc,
            tokens.Value.RefreshToken,
            tokens.Value.RefreshTokenExpiresAtUtc
        ));
    }
}
