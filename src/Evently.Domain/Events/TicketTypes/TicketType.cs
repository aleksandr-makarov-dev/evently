using Evently.Domain.Abstractions;
using Evently.Domain.Events.Events;

namespace Evently.Domain.Events.TicketTypes;

public sealed class TicketType : Entity
{
    public Guid EventId { get;  private set; }

    public string Name { get; private set; }

    public decimal Price { get; private set;}

    public decimal Quantity { get; private set; }

    public static TicketType Create(Event @event, string name, decimal price, decimal quantity)
    {
        var ticketType = new TicketType
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            Quantity = quantity,
            EventId = @event.Id
        };

        return ticketType;
    }
}
