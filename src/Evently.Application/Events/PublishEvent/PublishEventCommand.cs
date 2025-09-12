using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
