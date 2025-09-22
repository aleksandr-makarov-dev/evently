using Evently.Application.Abstractions.Authentication;
using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Emails;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityService identityService,
    IApplicationDbContext context,
    ILogger<RegisterUserCommandHandler> logger,
    IEmailSenderService emailSenderService
) : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private const string BaseUrl = "http://localhost:5173";

    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<string> identityId = await identityService.RegisterUserAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (identityId.IsFailure)
        {
            logger.LogError("Failed to register user with error: {Error}", identityId.Error);

            return Result.Failure<RegisterUserResponse>(identityId.Error);
        }

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            identityId.Value
        );

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        Result<string> token =
            await identityService.GenerateEmailConfirmationTokenAsync(request.Email, cancellationToken);

        if (token.IsFailure)
        {
            return Result.Failure<RegisterUserResponse>(token.Error);
        }

        string confirmationLink = $"{BaseUrl}/users/confirm-email?userId={user.Id}&code={token.Value}";

        string htmlBody = $"""
                           Здравствуйте, {request.FirstName}!
                           </br></br>
                           Спасибо за регистрацию в Evently. Чтобы завершить процесс, пожалуйста, подтвердите свой email по ссылке:
                           </br></br>
                           <a href="{confirmationLink}">{confirmationLink}</a>
                           </br></br>
                           Если вы не регистрировались в Evently, просто проигнорируйте это письмо.
                           """;


        await emailSenderService.SendEmailAsync(request.Email, "Завершите регистрацию", htmlBody);

        return Result.Success(new RegisterUserResponse(user.Email, false));
    }
}
