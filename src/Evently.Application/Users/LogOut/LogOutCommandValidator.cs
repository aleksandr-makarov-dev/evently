using FluentValidation;

namespace Evently.Application.Users.LogOut;

internal sealed class LogOutCommandValidator : AbstractValidator<LogOutCommand>
{
    public LogOutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
