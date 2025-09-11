using Evently.Application.Categories.GetCategories;
using Evently.Application.Events.GetEvents;

namespace Evently.Application.Events.GetEvent;

public record EventDetailsResponse(
    Guid Id,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc,
    CategoryResponse Category,
    List<TicketTypeResponse> TicketTypes
);
