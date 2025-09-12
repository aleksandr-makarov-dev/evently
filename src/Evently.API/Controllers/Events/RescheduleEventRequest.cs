namespace Evently.API.Controllers.Events;

public sealed record RescheduleEventRequest(DateTime StartsAtUtc, DateTime? EndsAtUtc);
