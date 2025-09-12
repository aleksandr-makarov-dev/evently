using Bogus;
using Evently.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();

    private readonly IServiceScope _scope;
    protected readonly IntegrationTestWebAppFactory Factory;
    protected readonly ISender Sender;
    protected readonly IApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Factory = factory;
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    }

    protected async Task CleanUpAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM evently.public.ticket_types;
            DELETE FROM evently.public.events;
            DELETE FROM evently.public.categories;
            """
        );
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
