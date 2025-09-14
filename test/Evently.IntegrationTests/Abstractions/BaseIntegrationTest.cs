using Bogus;
using Evently.Application.Abstractions.Data;
using Evently.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();

    private readonly IServiceScope _scope;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    
    protected readonly IntegrationTestWebAppFactory Factory;
    protected readonly ISender Sender;
    protected readonly IApplicationDbContext DbContext;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        HttpClient = factory.CreateClient();
        Factory = factory;
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _roleManager = _scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    }

    protected async Task CleanUpAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM evently.public.ticket_types;
            DELETE FROM evently.public.events;
            DELETE FROM evently.public.categories;
            DELETE FROM evently.public.user_roles;
            DELETE FROM evently.public.users;
            """
        );
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
