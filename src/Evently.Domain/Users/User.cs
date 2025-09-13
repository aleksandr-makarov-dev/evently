using Microsoft.AspNetCore.Identity;

namespace Evently.Domain.Users;

public sealed class User : IdentityUser<Guid>
{
    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public static User Create(string firstName, string lastName, string email)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = email,
        };
    }
}
