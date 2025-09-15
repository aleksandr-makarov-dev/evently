using Evently.Domain.Abstractions;

namespace Evently.Domain.Users;

public sealed class User : Entity
{
    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string IdentityId { get; init; } = string.Empty;

    public static User Create(string firstName, string lastName, string email, string identityId)
    {
        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            IdentityId = identityId,
        };
    }
}
