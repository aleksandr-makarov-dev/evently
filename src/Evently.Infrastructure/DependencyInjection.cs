using System.Text;
using Evently.Application.Abstractions.Clock;
using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Emails;
using Evently.Application.Abstractions.Identity;
using Evently.Infrastructure.Clock;
using Evently.Infrastructure.Data;
using Evently.Infrastructure.Emails;
using Evently.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Evently.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddMemoryCache();

        services.AddApplicationDbContext(configuration);

        services.AddIdentity(configuration);

        services.AddEmailSender(configuration);

        services.AddAuthentication(configuration);

        services.AddAuthorization();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtOptions jwtAuthOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        if (jwtAuthOptions is null)
        {
            throw new ApplicationException("Authentication options are null");
        }

        services.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtAuthOptions.Issuer,
                    ValidAudience = jwtAuthOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthOptions.Key)),
                    ValidateLifetime = true,
                    // TODO: access token expires 2 minutes before actual expiration time.
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });
    }

    private static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }

    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddDbContext<ApplicationIdentityDbContext>((options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddTransient<ITokenProvider, TokenProvider>();
    }

    private static void AddEmailSender(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));

        services.AddScoped<IEmailSenderService, EmailSenderService>();
    }
}
