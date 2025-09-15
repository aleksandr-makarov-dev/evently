using Evently.Application.Abstractions.Messaging;
using Evently.Application.Users.LoginUser;

namespace Evently.Application.Users.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<TokenResponse>;
