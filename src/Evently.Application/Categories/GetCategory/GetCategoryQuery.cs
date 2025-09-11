using Evently.Application.Abstractions.Messaging;
using Evently.Application.Categories.GetCategories;

namespace Evently.Application.Categories.GetCategory;

public record GetCategoryQuery(Guid Id) : IQuery<CategoryResponse>;
