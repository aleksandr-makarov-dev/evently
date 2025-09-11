namespace Evently.API.Controllers.Events;

public record CreateEventRequest(
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc
);
