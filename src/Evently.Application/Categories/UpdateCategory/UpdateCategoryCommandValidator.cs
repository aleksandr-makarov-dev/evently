using FluentValidation;

namespace Evently.Application.Categories.UpdateCategory;

internal sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
