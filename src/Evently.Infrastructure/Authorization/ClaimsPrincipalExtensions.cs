using System.Security.Claims;

namespace Evently.Infrastructure.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static string? GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
