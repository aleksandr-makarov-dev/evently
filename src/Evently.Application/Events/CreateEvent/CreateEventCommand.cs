using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Events.CreateEvent;

public record CreateEventCommand(
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc
) : ICommand<Guid>;
