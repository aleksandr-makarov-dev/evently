using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.TicketTypes.DeleteTicketType;

public record DeleteTicketTypeCommand(Guid TicketTypeId) : ICommand;
