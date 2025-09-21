namespace Evently.Application.Users.GetCurrentUser;

public sealed record CurrentUserResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email
);
