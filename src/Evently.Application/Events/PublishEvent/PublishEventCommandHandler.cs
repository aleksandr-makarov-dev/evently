using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Events.PublishEvent;

internal sealed class PublishEventCommandHandler(
    IApplicationDbContext context,
    ILogger<PublishEventCommandHandler> logger) : ICommandHandler<PublishEventCommand>
{
    public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await context.Events
            .FirstOrDefaultAsync(x => x.Id == request.EventId, cancellationToken: cancellationToken);

        if (@event is null)
        {
            logger.LogWarning("The event with id = {eventId} was not found", request.EventId);

            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if (!await context.TicketTypes.AnyAsync(x => x.EventId == request.EventId,
                cancellationToken: cancellationToken))
        {
            logger.LogWarning("To publish event it must have at least one ticket type");

            return Result.Failure(EventErrors.NoTicketsFound);
        }

        @event.Publish();

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
