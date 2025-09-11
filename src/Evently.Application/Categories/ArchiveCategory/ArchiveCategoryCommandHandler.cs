using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Categories.ArchiveCategory;

internal sealed class ArchiveCategoryCommandHandler(
    IApplicationDbContext context,
    ILogger<ArchiveCategoryCommandHandler> logger)
    : ICommandHandler<ArchiveCategoryCommand>
{
    public async Task<Result> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = context.Categories
            .FirstOrDefault(x => x.Id == request.CategoryId);

        if (category is null)
        {
            logger.LogError("Category with id {RequestCategoryId} was not found", request.CategoryId);

            return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
        }

        if (category.IsArchived)
        {
            logger.LogError("Category with id {RequestCategoryId} is already archived", request.CategoryId);

            return Result.Failure(CategoryErrors.AlreadyArchived);
        }

        category.Archive();

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
