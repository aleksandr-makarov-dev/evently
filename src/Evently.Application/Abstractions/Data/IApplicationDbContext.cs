using Evently.Domain.Events.Categories;
using Evently.Domain.Events.Events;
using Evently.Domain.Events.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Evently.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }

    DbSet<TicketType> TicketTypes { get; }

    DbSet<Event> Events { get; }
    
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    
}
