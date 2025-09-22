using Evently.Application.Abstractions.Authentication;
using Evently.Infrastructure.Authorization;
using Evently.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.Authentication;

public class PermissionManager(ApplicationIdentityDbContext context) : IPermissionManager
{
    public async Task<List<string?>> GetPermissionsAsync(string userId, CancellationToken cancellationToken = default)
    {
        List<string> roleIds = await context.UserRoles
            .Where(x => x.UserId == userId)
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken: cancellationToken);

        List<string?> permissions = await context.RoleClaims
            .Where(x => roleIds.Contains(x.RoleId))
            .Where(x => x.ClaimType == SecurityClaimTypes.Permission)
            .Select(x => x.ClaimValue)
            .Distinct()
            .ToListAsync(cancellationToken: cancellationToken);
        
        return permissions;
    }
}
