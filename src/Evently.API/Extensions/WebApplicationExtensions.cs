using Evently.Domain.Users;
using Evently.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Evently.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        RoleManager<IdentityRole> roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(Role.Member.Name))
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Member.Name));
        }

        if (!await roleManager.RoleExistsAsync(Role.Administrator.Name))
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Administrator.Name));
        }
    }
}
