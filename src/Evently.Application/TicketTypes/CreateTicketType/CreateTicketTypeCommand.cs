using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.TicketTypes.CreateTicketType;

public record CreateTicketTypeCommand(
    Guid EventId,
    string Name,
    decimal Price,
    decimal Quantity
) : ICommand<Guid>;
