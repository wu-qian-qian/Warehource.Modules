using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plc.Domain.S7;
using Plc.Infrastructure.Database;

namespace Plc.Infrastructure.db;

public class EntityItemConfiguration : IEntityTypeConfiguration<S7EntityItem>
{
    public void Configure(EntityTypeBuilder<S7EntityItem> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(S7EntityItem));
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.Property(e => e.Name).IsRequired()
            .HasMaxLength(20);
        builder.Property(e => e.Ip).IsRequired()
            .HasMaxLength(20);
        builder.Property(e => e.Description).IsRequired(false).HasMaxLength(50);
    }
}