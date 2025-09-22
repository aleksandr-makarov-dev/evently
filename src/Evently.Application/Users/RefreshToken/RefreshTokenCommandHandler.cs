using Evently.Application.Abstractions.Authentication;
using Evently.Application.Abstractions.Messaging;
using Evently.Application.Users.LoginUser;
using Evently.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
    IIdentityService identityService,
    ILogger<RefreshTokenCommandHandler> logger
) : ICommandHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Result<TokensModel> tokens = await identityService.RefreshTokenAsync(
            request.RefreshToken,
            cancellationToken
        );

        if (tokens.IsFailure)
        {
            logger.LogWarning("Refresh token attempt failed for token {RefreshToken}. Error: {Error}",
                request.RefreshToken,
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
