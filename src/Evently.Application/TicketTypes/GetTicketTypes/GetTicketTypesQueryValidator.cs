using FluentValidation;

namespace Evently.Application.TicketTypes.GetTicketTypes;

internal sealed class GetTicketTypesQueryValidator : AbstractValidator<GetTicketTypesQuery>
{
    public GetTicketTypesQueryValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty();
    }
}
