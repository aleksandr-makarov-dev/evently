using Evently.Domain.Abstractions;

namespace Evently.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid eventId) =>
        Error.NotFound("Users.NotFound", $"The user with the identifier {eventId} was not found");

    public static Error IdentityError(string error) => Error.Conflict("Users.IdentityError", error);
}
