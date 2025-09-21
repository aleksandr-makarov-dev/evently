using Evently.Domain.Abstractions;

namespace Evently.Domain.Users;

public static class UserErrors
{
    public static Error UserNotFound(Guid userId) =>
        Error.NotFound(
            "Users.UserNotFound",
            $"User with id '{userId}' was not found.");

    public static readonly Error Unauthorized = Error.Unauthorized(
        "Users.Unauthorized",
        "Failed to authorize user.");
}
