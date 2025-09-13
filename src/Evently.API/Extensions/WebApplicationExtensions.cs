using Evently.Domain.Users;
using Evently.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Evently.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        RoleManager<IdentityRole<Guid>> roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (!await roleManager.RoleExistsAsync(Role.Member.Name))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Member.Name));
        }

        if (!await roleManager.RoleExistsAsync(Role.Administrator.Name))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Administrator.Name));
        }
    }
}
