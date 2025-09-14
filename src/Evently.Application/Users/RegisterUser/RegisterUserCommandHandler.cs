using System.Text;
using System.Text.Encodings.Web;
using Evently.Application.Abstractions.Emails;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    UserManager<User> userManager,
    IEmailSenderService<User> emailSender,
    ILogger<RegisterUserCommandHandler> logger)
    : ICommandHandler<RegisterUserCommand>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName, request.Email);

        IdentityResult createUserResult = await userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
        {
            string error = createUserResult.Errors.First().Description;

            logger.LogWarning("User registration failed with error: {error}", error);

            return Result.Failure<Guid>(UserErrors.RegistrationFailed(error));
        }

        IdentityResult addRoleResult = await userManager.AddToRoleAsync(user, Role.Member.Name);
        if (!addRoleResult.Succeeded)
        {
            string error = createUserResult.Errors.First().Description;

            logger.LogError("Failed to add user {UserId} to role {Role} with error: {error}",
                user.Id, Role.Member.Name, error);

            return Result.Failure<Guid>(UserErrors.RegistrationFailed(error));
        }

        string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        string encodedCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        string confirmationLink = $"/confirm?userId={user.Id}&code={encodedCode}";

        await emailSender.SendConfirmationLinkAsync(user, user.Email!, HtmlEncoder.Default.Encode(confirmationLink));

        return Result.Success();
    }
}
