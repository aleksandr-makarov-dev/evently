using Microsoft.AspNetCore.Authorization;

namespace Evently.Infrastructure.Authorization;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
