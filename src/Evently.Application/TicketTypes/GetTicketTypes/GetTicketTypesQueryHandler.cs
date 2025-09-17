using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Application.Events.GetEvent;
using Evently.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Evently.Application.TicketTypes.GetTicketTypes;

internal sealed class GetTicketTypesQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetTicketTypesQuery, List<TicketTypeResponse>>
{
    public async Task<Result<List<TicketTypeResponse>>> Handle(GetTicketTypesQuery request,
        CancellationToken cancellationToken)
    {
        List<TicketTypeResponse> ticketTypes = await context.TicketTypes
            .AsNoTracking()
            .Where(x => x.EventId == request.EventId)
            .Select(x => new TicketTypeResponse(
                x.Id,
                x.Name,
                x.Price,
                x.Quantity
            ))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(ticketTypes);
    }
}
