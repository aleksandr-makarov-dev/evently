namespace Evently.Application.Events.GetEvent;

public record TicketTypeResponse(
    Guid Id,
    string Name,
    decimal Price,
    decimal Quantity
);
