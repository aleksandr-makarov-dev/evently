using FluentValidation;

namespace Evently.Application.Events.CancelEvent;

internal sealed class PublishEventCommandValidator : AbstractValidator<CancelEventCommand>
{
    public PublishEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty();
    }
}
