using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Events;
using Evently.Domain.Events.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.TicketTypes.CreateTicketType;

internal sealed class CreateTicketTypeCommandHandler(
    IApplicationDbContext context,
    ILogger<CreateTicketTypeCommandHandler> logger
) : ICommandHandler<CreateTicketTypeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await context.Events
            .FirstOrDefaultAsync(x => x.Id == request.EventId, cancellationToken);

        if (@event is null)
        {
            logger.LogWarning("The event with the identifier {eventId} was not found", request.EventId);

            return Result.Failure<Guid>(EventErrors.NotFound(request.EventId));
        }

        var ticketType = TicketType.Create(
            @event,
            request.Name,
            request.Price,
            request.Quantity
        );

        context.TicketTypes.Add(ticketType);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(ticketType.Id);
    }
}
