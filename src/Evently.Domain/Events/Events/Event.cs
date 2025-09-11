using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;
using Evently.Domain.Events.TicketTypes;

namespace Evently.Domain.Events.Events;

public sealed class Event : Entity
{
    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Location { get; private set; }

    public DateTime StartsAtUtc { get; private set; }

    public DateTime? EndsAtUtc { get; private set; }

    public EventStatus Status { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public List<TicketType> TicketTypes { get; private set; }

    public static Result<Event> Create(
        Category category,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        if (endsAtUtc.HasValue && endsAtUtc.Value < startsAtUtc)
        {
            return Result.Failure<Event>(EventErrors.EndDatePrecedesStartDate);
        }

        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc,
            CategoryId = category.Id,
            Status = EventStatus.Draft
        };

        return @event;
    }
}
