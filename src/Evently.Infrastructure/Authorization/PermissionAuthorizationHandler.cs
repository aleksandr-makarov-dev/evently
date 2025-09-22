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

        bool hasPermission = context.User.Claims
            .Any(x => x.Type == SecurityClaimTypes.Permission && x.Value == requirement.Permission);

        if (hasPermission)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
