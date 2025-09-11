using Evently.Domain.Events.Categories;
using Evently.Domain.Events.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Infrastructure.Data.Configurations;

internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Location)
            .HasMaxLength(250);

        builder.HasOne<Category>(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId);
    }
}
