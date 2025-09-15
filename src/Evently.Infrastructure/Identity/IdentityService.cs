using Evently.Application.Abstractions.Clock;
using Evently.Application.Abstractions.Identity;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evently.Infrastructure.Identity;

internal sealed class IdentityService(
    UserManager<ApplicationUser> userManager,
    ITokenProvider tokenProvider,
    IOptions<JwtOptions> jwtOptions,
    IDateTimeProvider dateTimeProvider,
    ILogger<IdentityService> logger
) : IIdentityService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<Result<string>> RegisterUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var newUser = new ApplicationUser
        {
            Email = email,
            UserName = email,
            EmailConfirmed = true
        };

        IdentityResult createResult = await userManager.CreateAsync(newUser, password);

        if (!createResult.Succeeded)
        {
            string error = createResult.Errors.First().Description;

            logger.LogError("Failed to register user with email {Email}. Error: {Error}",
                email,
                error);

            return Result.Failure<string>(IdentityErrors.EmailIsNotUnique);
        }

        IdentityResult roleAssignResult = await userManager.AddToRoleAsync(newUser, Role.Member.Name);

        if (!roleAssignResult.Succeeded)
        {
            string error = roleAssignResult.Errors.First().Description;

            logger.LogError("Failed to assign role {Role} to user {UserId}. Error: {Error}",
                Role.Member.Name,
                newUser.Id,
                error);

            return Result.Failure<string>(IdentityErrors.AssignToRoleFailure);
        }

        return Result.Success(newUser.Id);
    }

    public async Task<Result<TokenModel>> LoginUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            logger.LogWarning("Login failed: user with email {Email} not found.", email);
            return Result.Failure<TokenModel>(IdentityErrors.InvalidCredentials);
        }

        bool passwordValid = await userManager.CheckPasswordAsync(user, password);

        if (!passwordValid)
        {
            logger.LogWarning("Login failed: invalid password for user {UserId} ({Email}).", user.Id, email);
            return Result.Failure<TokenModel>(IdentityErrors.InvalidCredentials);
        }

        TokenModel tokens = tokenProvider.Create(new IdentityModel(user.Id, user.Email!, []));

        DateTime expiresAtUtc = dateTimeProvider.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresInDays);

        IdentityResult updateResult =
            await userManager.UpdateRefreshTokenAsync(user, tokens.RefreshToken, expiresAtUtc);

        if (!updateResult.Succeeded)
        {
            string error = updateResult.Errors.First().Description;

            logger.LogError("Failed to update refresh token for user {UserId}. Error: {Error}",
                user.Id,
                error);

            return Result.Failure<TokenModel>(IdentityErrors.InvalidRefreshToken);
        }

        return Result.Success(tokens);
    }

    public async Task<Result<TokenModel>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByRefreshTokenAsync(refreshToken);

        if (user is null)
        {
            logger.LogWarning("Refresh token failed: no user found with given refresh token.");
            return Result.Failure<TokenModel>(IdentityErrors.InvalidRefreshToken);
        }

        if (user.RefreshTokenExpiresAtUtc < dateTimeProvider.UtcNow)
        {
            logger.LogWarning("Refresh token failed: provided refresh token has expired.");

            return Result.Failure<TokenModel>(IdentityErrors.InvalidRefreshToken);
        }

        TokenModel tokens = tokenProvider.Create(new IdentityModel(user.Id, user.Email!, []));

        DateTime expiresAtUtc = dateTimeProvider.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresInDays);

        IdentityResult updateResult =
            await userManager.UpdateRefreshTokenAsync(user, tokens.RefreshToken, expiresAtUtc);

        if (!updateResult.Succeeded)
        {
            string error = updateResult.Errors.First().Description;

            logger.LogError("Failed to update refresh token for user {UserId}. Error: {Error}",
                user.Id,
                error);

            return Result.Failure<TokenModel>(IdentityErrors.InvalidRefreshToken);
        }

        return Result.Success(tokens);
    }
}
