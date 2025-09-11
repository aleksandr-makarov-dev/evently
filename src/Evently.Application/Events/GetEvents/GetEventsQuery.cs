using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Events.Events;

namespace Evently.Application.Events.GetEvents;

public record GetEventsQuery() : IQuery<IReadOnlyList<EventResponse>>;
