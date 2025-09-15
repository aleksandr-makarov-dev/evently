namespace Evently.Application.Users.RegisterUser;

public record RegisterUserResponse(
    string Email,
    bool EmailConfirmed
);
