using System.Security.Claims;
using Evently.API.Infrastructure;
using Evently.Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Evently.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        RoleManager<IdentityRole> roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(RoleNames.Member))
        {
            var memberRole = new IdentityRole(RoleNames.Member);

            await roleManager.CreateAsync(memberRole);

            await roleManager.AddClaimAsync(memberRole,
                new Claim(PermissionClaimTypes.Permission, PermissionNames.GetUser));

            await roleManager.AddClaimAsync(memberRole,
                new Claim(PermissionClaimTypes.Permission, PermissionNames.GetEvents));
        }

        if (!await roleManager.RoleExistsAsync(RoleNames.Administrator))
        {
            await roleManager.CreateAsync(new IdentityRole(RoleNames.Administrator));
        }
    }
}
