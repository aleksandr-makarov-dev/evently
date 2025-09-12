using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
