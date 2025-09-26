using Evently.Domain.Attachments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Infrastructure.Data.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("attachments");

        builder.HasKey(e => e.Id);

        builder.Property(x => x.ContentType)
            .HasMaxLength(255);

        builder.HasIndex(x => x.Key)
            .IsUnique();
    }
}
