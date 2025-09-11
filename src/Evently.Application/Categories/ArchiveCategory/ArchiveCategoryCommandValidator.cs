using FluentValidation;

namespace Evently.Application.Categories.ArchiveCategory;

internal sealed class ArchiveCategoryCommandValidator : AbstractValidator<ArchiveCategoryCommand>
{
    public ArchiveCategoryCommandValidator()
    {
        RuleFor(x=>x.CategoryId)
            .NotEmpty();
    }
}
