using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Events.RescheduleEvent;

internal sealed class RescheduleEventCommandHandler(
    IApplicationDbContext context,
    ILogger<RescheduleEventCommandHandler> logger) : ICommandHandler<RescheduleEventCommand>
{
    public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await context.Events
            .FirstOrDefaultAsync(x => x.Id == request.EventId, cancellationToken: cancellationToken);

        if (@event is null)
        {
            logger.LogWarning("The event with id = {eventId} was not found", request.EventId);

            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        @event.Reschedule(request.StartsAtUtc, request.EndsAtUtc);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
