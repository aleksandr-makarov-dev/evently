using Microsoft.AspNetCore.Identity;

namespace Evently.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiresAtUtc { get; set; }
}
