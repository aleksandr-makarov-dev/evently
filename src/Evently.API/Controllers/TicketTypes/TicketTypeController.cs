using Evently.API.Infrastructure;
using Evently.Application.TicketTypes.CreateTicketType;
using Evently.Application.TicketTypes.DeleteTicketType;
using Evently.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers.TicketTypes;

[ApiController]
[Route("api/ticket-types")]
public class TicketTypeController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTicketType([FromBody] CreateTicketTypeRequest request)
    {
        var command = new CreateTicketTypeCommand(
            request.EventId,
            request.Name,
            request.Price,
            request.Quantity
        );

        Result<Guid> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTicketType(Guid id)
    {
        var command = new DeleteTicketTypeCommand(id);

        Result result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return NoContent();
    }
}
