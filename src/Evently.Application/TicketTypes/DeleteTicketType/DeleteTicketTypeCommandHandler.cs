using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.TicketTypes.DeleteTicketType;

internal class DeleteTicketTypeCommandHandler(
    IApplicationDbContext context,
    ILogger<DeleteTicketTypeCommandHandler> logger
) : ICommandHandler<DeleteTicketTypeCommand>
{
    public async Task<Result> Handle(DeleteTicketTypeCommand request, CancellationToken cancellationToken)
    {
        TicketType? ticketType = await context.TicketTypes
            .FirstOrDefaultAsync(x => x.Id == request.TicketTypeId, cancellationToken: cancellationToken);

        if (ticketType is null)
        {
            logger.LogError("The ticket type with id = {ticketTypeId} was not found}", request.TicketTypeId);

            return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        context.TicketTypes.Remove(ticketType);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
