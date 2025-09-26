using Evently.Domain.Attachments;
using Evently.Domain.Events.Categories;
using Evently.Domain.Events.Events;
using Evently.Domain.Events.TicketTypes;
using Evently.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Evently.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }

    DbSet<TicketType> TicketTypes { get; }

    DbSet<Event> Events { get; }

    DbSet<User> Users { get; }

    DbSet<Attachment> Attachments { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
