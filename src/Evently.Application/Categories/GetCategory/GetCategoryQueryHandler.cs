using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Application.Categories.GetCategories;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler(
    IApplicationDbContext context,
    ILogger<GetCategoryQueryHandler> logger)
    : IQueryHandler<GetCategoryQuery, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        CategoryResponse? category = await context.Categories
            .Where(x => x.Id == request.Id)
            .Select(x => new CategoryResponse(x.Id, x.Name, x.IsArchived))
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        if (category is null)
        {
            logger.LogWarning("The category with the identifier {RequestId} was not found", request.Id);
            
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(request.Id));
        }

        return category;
    }
}
