namespace Evently.Application.Abstractions.Identity;

using Evently.Domain.Abstractions;

public static class IdentityErrors
{
    public static Error EmailIsNotUnique(string email) =>
        Error.Conflict(
            "Identity.EmailIsNotUnique",
            $"The email '{email}' is already taken.");

    public static Error AssignToRoleFailure(string role) =>
        Error.Problem(
            "Identity.AssignToRoleFailure",
            $"Failed to assign role '{role}' to user.");

    public static Error EmailIsNotVerified(string email) =>
        Error.Problem("Identity.EmailIsNotVerified",
            $"The email '{email}' is not verified.");

    public static Error InvalidCredentials() =>
        Error.Unauthorized(
            "Identity.InvalidCredentials",
            "Provided invalid email or password.");

    public static Error InvalidRefreshToken() =>
        Error.Unauthorized(
            "Identity.InvalidRefreshToken",
            "Provided refresh token is not valid.");

    public static Error ExpiredRefreshToken() =>
        Error.Unauthorized(
            "Identity.ExpiredRefreshToken",
            "Provided refresh token has expired.");

    public static Error UserNotFound(Guid userId) =>
        Error.NotFound(
            "Identity.UserNotFound",
            $"User with id '{userId}' was not found.");

    public static Error UserNotFoundByEmail(string email) =>
        Error.NotFound(
            "Identity.UserNotFound",
            $"User with email '{email}' was not found.");

    public static Error EmailConfirmationFailure(string email) =>
        Error.Problem(
            "Identity.EmailConfirmationFailure",
            $"Email '{email}' confirmation failed. Provided email or code is not valid.");
}
