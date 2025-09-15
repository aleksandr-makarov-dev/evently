using Evently.Domain.Abstractions;

namespace Evently.Application.Abstractions.Identity;

public static class IdentityErrors
{
    public static readonly Error EmailIsNotUnique = Error.Conflict(
        "Identity.EmailIsNotUnique",
        "The specified email is not unique.");

    public static readonly Error AssignToRoleFailure = Error.Failure(
        "Identity.AssignToRoleFailure", "Failed to assign role to user");

    public static readonly Error InvalidCredentials = Error.Problem(
        "Identity.InvalidCredentials", "Provided invalid email or password");

    public static readonly Error InvalidRefreshToken = Error.Conflict(
        "Identity.InvalidRefreshToken", "Provided token is not valid");
}
