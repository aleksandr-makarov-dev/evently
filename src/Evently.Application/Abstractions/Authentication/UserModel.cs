namespace Evently.Application.Abstractions.Authentication;

public sealed record UserModel(string FirstName, string LastName, string Email, string Password);
