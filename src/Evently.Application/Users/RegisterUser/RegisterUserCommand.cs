using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<RegisterUserResponse>;
