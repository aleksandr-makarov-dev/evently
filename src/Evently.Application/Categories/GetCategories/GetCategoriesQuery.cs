using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Categories.GetCategories;

public record GetCategoriesQuery : IQuery<IReadOnlyList<CategoryResponse>>;
