using Microsoft.AspNetCore.Authorization;

namespace Evently.Infrastructure.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Identity is not { IsAuthenticated: true })
        {
            return Task.CompletedTask;
        }

        // TODO: to make check secure send request to database to get up-to-date permissions.
        
        bool hasPermission = context.User.Claims
            .Any(x => x.Type == PermissionClaimTypes.Permission && x.Value == requirement.Permission);

        if (hasPermission)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
