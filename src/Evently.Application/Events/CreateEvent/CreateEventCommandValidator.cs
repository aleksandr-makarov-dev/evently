using FluentValidation;

namespace Evently.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();

        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Location)
            .MaximumLength(250);

        RuleFor(x => x.EndsAtUtc)
            .Must((x, endsAt) => endsAt > x.StartsAtUtc)
            .When(x => x.EndsAtUtc.HasValue);
    }
}
