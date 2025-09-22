using Evently.Application.Abstractions.Authentication;
using Evently.Application.Abstractions.Clock;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evently.Infrastructure.Authentication;

internal sealed class IdentityService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ITokenProvider tokenProvider,
    IOptions<JwtOptions> jwtOptions,
    IDateTimeProvider dateTimeProvider,
    ILogger<IdentityService> logger,
    IPermissionManager permissionManager
) : IIdentityService
{
    public RoleManager<IdentityRole> RoleManager { get; } = roleManager;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<Result<string>> RegisterUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var newUser = new ApplicationUser
        {
            Email = email,
            UserName = email
        };

        IdentityResult createResult = await userManager.CreateAsync(newUser, password);

        if (!createResult.Succeeded)
        {
            logger.LogError("Failed to register user with email {Email}", email);
            return Result.Failure<string>(IdentityErrors.EmailIsNotUnique(email));
        }

        IdentityResult roleAssignResult = await userManager.AddToRoleAsync(newUser, Role.Member.Name);

        if (!roleAssignResult.Succeeded)
        {
            logger.LogError("Failed to assign role {Role} to user {UserId}",
                Role.Member.Name, newUser.Id);

            return Result.Failure<string>(IdentityErrors.AssignToRoleFailure(Role.Member.Name));
        }

        return Result.Success(newUser.Id);
    }


    public async Task<Result<TokensModel>> LoginUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            logger.LogWarning("Login failed: user with email {Email} not found.", email);
            return Result.Failure<TokensModel>(IdentityErrors.InvalidCredentials());
        }

        if (!await userManager.CheckPasswordAsync(user, password))
        {
            logger.LogWarning("Login failed: invalid password for user {UserId} ({Email}).",
                user.Id, email);
            return Result.Failure<TokensModel>(IdentityErrors.InvalidCredentials());
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            logger.LogWarning("Login failed: email not confirmed for user {Email}.", email);
            return Result.Failure<TokensModel>(IdentityErrors.EmailIsNotVerified(email));
        }

        return await GenerateAndPersistTokensAsync(user, cancellationToken);
    }

    public async Task<Result<TokensModel>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByRefreshTokenAsync(refreshToken);

        if (user is null)
        {
            logger.LogWarning("Refresh token failed: no user found with given refresh token.");
            return Result.Failure<TokensModel>(IdentityErrors.InvalidRefreshToken());
        }

        if (user.RefreshTokenExpiresAtUtc < dateTimeProvider.UtcNow)
        {
            logger.LogWarning("Refresh token failed: provided refresh token has expired.");
            return Result.Failure<TokensModel>(IdentityErrors.ExpiredRefreshToken());
        }

        return await GenerateAndPersistTokensAsync(user, cancellationToken);
    }

    public async Task<Result> LogOutUserAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByRefreshTokenAsync(refreshToken);

        if (user is null)
        {
            logger.LogWarning("Logout failed: no user found with given refresh token.");
            return Result.Failure(IdentityErrors.InvalidRefreshToken());
        }

        IdentityResult updateResult = await userManager.UpdateRefreshTokenAsync(user, null, null);

        if (!updateResult.Succeeded)
        {
            logger.LogError("Failed to clear refresh token for user {UserId}", user.Id);
            return Result.Failure(IdentityErrors.InvalidRefreshToken());
        }

        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(Guid userId,
        string code,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return Result.Failure(IdentityErrors.UserNotFound(userId));
        }

        IdentityResult confirmEmailResult = await userManager.ConfirmEmailAsync(user, code);

        if (!confirmEmailResult.Succeeded)
        {
            logger.LogError("Failed to confirm email {Email}", user.Email);
            return Result.Failure(IdentityErrors.EmailConfirmationFailure(user.Email!));
        }

        return Result.Success();
    }

    public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result.Failure<string>(IdentityErrors.UserNotFoundByEmail(email));
        }

        string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        return Result.Success(token);
    }

    private async Task<Result<TokensModel>> GenerateAndPersistTokensAsync(
        ApplicationUser user,
        CancellationToken cancellationToken = default)
    {
        IList<string> roles = await userManager.GetRolesAsync(user);
        List<string?> permissions = await permissionManager.GetPermissionsAsync(user.Id, cancellationToken);

        TokensModel tokens = tokenProvider.Create(
            new IdentityModel(user.Id, user.Email!, roles, permissions!));

        DateTime expiresAtUtc = dateTimeProvider.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresInDays);

        IdentityResult updateResult =
            await userManager.UpdateRefreshTokenAsync(user, tokens.RefreshToken, expiresAtUtc);

        if (!updateResult.Succeeded)
        {
            logger.LogError("Failed to update refresh token for user {UserId}", user.Id);
            return Result.Failure<TokensModel>(IdentityErrors.InvalidRefreshToken());
        }

        return Result.Success(tokens);
    }
}
