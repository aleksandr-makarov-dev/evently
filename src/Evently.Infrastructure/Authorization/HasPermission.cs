using Microsoft.AspNetCore.Authorization;

namespace Evently.Infrastructure.Authorization;

public sealed class HasPermission(string permission) : AuthorizeAttribute(permission);
