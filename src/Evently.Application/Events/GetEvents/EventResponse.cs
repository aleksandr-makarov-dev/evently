using Evently.Application.Categories.GetCategories;

namespace Evently.Application.Events.GetEvents;

public record EventResponse(
    Guid Id,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc,
    CategoryResponse Category
);
