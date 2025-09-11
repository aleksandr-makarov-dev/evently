using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;
using Microsoft.EntityFrameworkCore;

namespace Evently.Application.Categories.GetCategories;

internal sealed class GetCategoriesQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetCategoriesQuery, IReadOnlyList<CategoryResponse>>
{
    public async Task<Result<IReadOnlyList<CategoryResponse>>> Handle(GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        List<CategoryResponse> categories = await context.Categories
            .AsNoTracking()
            .Select(x => new CategoryResponse(x.Id, x.Name, x.IsArchived))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success<IReadOnlyList<CategoryResponse>>(categories.AsReadOnly());
    }
}
