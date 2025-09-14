using System.Text;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(
    UserManager<User> userManager,
    ILogger<ConfirmEmailCommandHandler> logger) : ICommandHandler<ConfirmEmailCommand>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            logger.LogWarning("The user with id = {userId} was not found", request.UserId);

            return Result.Failure(UserErrors.EmailConfirmationFailed);
        }

        string decodedCode;

        try
        {
            decodedCode = Encoding.UTF8.GetString(Convert.FromBase64String(request.Code));
        }
        catch
        {
            logger.LogWarning("Invalid email confirmation code format for user {UserId}", request.UserId);
            return Result.Failure(UserErrors.EmailConfirmationFailed);
        }

        IdentityResult confirmEmailResult = await userManager.ConfirmEmailAsync(user, decodedCode);

        if (!confirmEmailResult.Succeeded)
        {
            logger.LogWarning("Email confirmation failed for user {UserId} with error: {error}", user.Id,
                confirmEmailResult.Errors.First().Description);
            return Result.Failure(UserErrors.EmailConfirmationFailed);
        }

        return Result.Success();
    }
}
