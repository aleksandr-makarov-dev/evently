using Evently.Application.Abstractions.Clock;
using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;
using Evently.Domain.Events.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
    IApplicationDbContext context,
    ILogger<CreateEventCommandHandler> logger,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure<Guid>(EventErrors.StartDateInPast);
        }

        Category? category = await context.Categories.FirstOrDefaultAsync(
            x => x.Id == request.CategoryId,
            cancellationToken: cancellationToken);

        if (category is null)
        {
            logger.LogError("The category with id {RequestCategoryId} was not found", request.CategoryId);

            return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
        }

        Result<Event> @event = Event.Create(
            category,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc
        );

        if (@event.IsFailure)
        {
            return Result.Failure<Guid>(@event.Error);
        }

        context.Events.Add(@event.Value);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(@event.Value.Id);
    }
}
