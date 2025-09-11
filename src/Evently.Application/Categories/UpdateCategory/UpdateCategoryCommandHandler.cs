using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Categories.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler(
    IApplicationDbContext context,
    ILogger<UpdateCategoryCommandHandler> logger) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = context.Categories
            .FirstOrDefault(x => x.Id == request.CategoryId);

        if (category is null)
        {
            logger.LogError("Category with id {RequestCategoryId} was not found", request.CategoryId);

            return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
        }

        category.ChangeName(request.Name);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
