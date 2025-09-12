using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(
    Guid EventId,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc) : ICommand;
