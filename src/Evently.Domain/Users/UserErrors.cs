using Evently.Domain.Abstractions;

namespace Evently.Domain.Users;

public static class UserErrors
{
    // Generic failure for email confirmation
    public static Error EmailConfirmationFailed =>
        Error.Unauthorized("Users.EmailConfirmationFailed", "Email confirmation failed.");

    // Identity conflicts (like duplicate email)
    public static Error RegistrationFailed(string error) =>
        Error.Conflict("Users.RegistrationFailed", error);

    // General unauthorized access
    public static Error Unauthorized =>
        Error.Unauthorized("Users.Unauthorized", "Unauthorized");
}
