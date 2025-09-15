using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.Identity;

internal static class UserManagerExtensions
{
    public static Task<ApplicationUser?> FindByRefreshTokenAsync(this UserManager<ApplicationUser> manager,
        string refreshToken)
    {
        return manager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
    }

    public static Task<IdentityResult> UpdateRefreshTokenAsync(this UserManager<ApplicationUser> manager,
        ApplicationUser user,
        string refreshToken, DateTime expiration)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAtUtc = expiration;

        return manager.UpdateAsync(user);
    }
}
