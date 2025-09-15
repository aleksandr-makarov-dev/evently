using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Identity;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityService identityService,
    IApplicationDbContext context,
    ILogger<RegisterUserCommandHandler> logger
) : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
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

        return Result.Success(new RegisterUserResponse(user.Email, false));
    }
}
