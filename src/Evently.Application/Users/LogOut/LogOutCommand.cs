using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Users.LogOut;

public sealed record LogOutCommand(string RefreshToken) : ICommand;
