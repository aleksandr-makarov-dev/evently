using Evently.API.Infrastructure;
using Evently.Application.Categories.ArchiveCategory;
using Evently.Application.Events.CreateEvent;
using Evently.Application.Events.GetEvent;
using Evently.Application.Events.GetEvents;
using Evently.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers.Events;

[ApiController]
[Route("api/events")]
public class EventsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
    {
        var command = new CreateEventCommand(
            request.CategoryId,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc
        );

        Result<Guid> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(new { id = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var query = new GetEventsQuery();

        Result<IReadOnlyList<EventResponse>> result = await sender.Send(query);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEvent([FromRoute] Guid id)
    {
        var query = new GetEventQuery(id);

        Result<EventDetailsResponse> result = await sender.Send(query);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }
}
