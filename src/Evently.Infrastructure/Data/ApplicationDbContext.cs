using Evently.Application.Abstractions.Data;
using Evently.Domain.Attachments;
using Evently.Domain.Events.Categories;
using Evently.Domain.Events.Events;
using Evently.Domain.Events.TicketTypes;
using Evently.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Category> Categories { get; set; }

    public DbSet<TicketType> TicketTypes { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Schemas.Application);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
