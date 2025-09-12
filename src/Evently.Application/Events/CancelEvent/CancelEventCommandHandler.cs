
using Evently.Application.Abstractions.Clock;
using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Events.CancelEvent;

internal sealed class CancelEventCommandHandler(
    IApplicationDbContext context,
    ILogger<CancelEventCommandHandler> logger,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CancelEventCommand>
{
    public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await context.Events
            .FirstOrDefaultAsync(x => x.Id == request.EventId, cancellationToken: cancellationToken);

        if (@event is null)
        {
            logger.LogWarning("The event with id = {eventId} was not found", request.EventId);

            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        Result result = @event.Cancel(dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
