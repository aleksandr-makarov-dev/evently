using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Evently.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(UserManager<User> userManager)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName, request.Email);

        // TODO: remove later
        user.EmailConfirmed = true;

        IdentityResult createUserResult = await userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
        {
            return Result.Failure<Guid>(UserErrors.IdentityError(createUserResult.Errors.First().Description));
        }

        await userManager.AddToRoleAsync(user, Role.Member.Name);

        // TODO: send confirmation email

        return Result.Success(user.Id);
    }
}
