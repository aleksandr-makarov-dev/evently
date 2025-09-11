using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Application.Categories.GetCategories;
using Evently.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Evently.Application.Events.GetEvents;

internal sealed class GetEventsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetEventsQuery, IReadOnlyList<EventResponse>>
{
    public async Task<Result<IReadOnlyList<EventResponse>>> Handle(GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        List<EventResponse> events = await context.Events
            .Select(x => new EventResponse(
                x.Id,
                x.Title,
                x.Description,
                x.Location,
                x.StartsAtUtc,
                x.EndsAtUtc,
                new CategoryResponse(x.Category.Id, x.Category.Name, x.Category.IsArchived)
            ))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success<IReadOnlyList<EventResponse>>(events);
    }
}
