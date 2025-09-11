using Evently.Application.Abstractions.Data;
using Evently.Domain.Events.Categories;
using Evently.Domain.Events.Events;
using Evently.Domain.Events.TicketTypes;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<TicketType> TicketTypes { get; set; }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
