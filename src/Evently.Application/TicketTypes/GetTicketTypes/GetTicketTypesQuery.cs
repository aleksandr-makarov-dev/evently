using Evently.Application.Abstractions.Messaging;
using Evently.Application.Events.GetEvent;

namespace Evently.Application.TicketTypes.GetTicketTypes;

public record GetTicketTypesQuery(Guid EventId) : IQuery<List<TicketTypeResponse>>;
