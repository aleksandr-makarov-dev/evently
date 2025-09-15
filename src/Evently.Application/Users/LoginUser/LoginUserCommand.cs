using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<TokenResponse>;
