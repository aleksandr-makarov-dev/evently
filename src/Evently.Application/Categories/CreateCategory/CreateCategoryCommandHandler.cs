using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Events.Categories;

namespace Evently.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);

        context.Categories.Add(category);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid>(category.Id);
    }
}
