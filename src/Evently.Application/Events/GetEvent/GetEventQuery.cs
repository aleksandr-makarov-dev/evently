using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Application.Categories.GetCategories;
using Evently.Application.Events.GetEvents;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Events;
using Microsoft.EntityFrameworkCore;

namespace Evently.Application.Events.GetEvent;

public record GetEventQuery(Guid Id) : IQuery<EventDetailsResponse>;

internal sealed class GetEventQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetEventQuery, EventDetailsResponse>
{
    public async Task<Result<EventDetailsResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        EventDetailsResponse? @event = await context.Events
            .Where(x => x.Id == request.Id)
            .Select(x => new EventDetailsResponse(
                x.Id,
                x.Title,
                x.Description,
                x.Location,
                x.StartsAtUtc,
                x.EndsAtUtc,
                new CategoryResponse(x.Category.Id, x.Category.Name, x.Category.IsArchived),
                x.TicketTypes
                    .Select(t => new TicketTypeResponse(
                            t.Id,
                            t.Name,
                            t.Price,
                            t.Quantity 
                        )
                    ).ToList()
            ))
            .SingleOrDefaultAsync(cancellationToken);

        if (@event is null)
        {
            return Result.Failure<EventDetailsResponse>(EventErrors.NotFound(request.Id));
        }

        return Result.Success(@event);
    }
}
