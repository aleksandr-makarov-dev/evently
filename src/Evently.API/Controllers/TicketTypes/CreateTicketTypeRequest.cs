namespace Evently.API.Controllers.TicketTypes;

public record CreateTicketTypeRequest(Guid EventId, string Name, decimal Price, decimal Quantity);
