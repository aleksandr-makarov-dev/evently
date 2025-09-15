using Evently.Application.Abstractions.Clock;
using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Emails;
using Evently.Application.Abstractions.Identity;
using Evently.Domain.Users;
using Evently.Infrastructure.Clock;
using Evently.Infrastructure.Data;
using Evently.Infrastructure.Emails;
using Evently.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddDbContext<ApplicationDbContext>((options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDbContext<ApplicationIdentityDbContext>((options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // Add Identity Core

        services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();

        services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));

        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddTransient<ITokenProvider, TokenProvider>();
    }
}
