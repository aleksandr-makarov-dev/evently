using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Users.ConfirmEmail;

public sealed record ConfirmEmailCommand(Guid UserId, string Code) : ICommand;
