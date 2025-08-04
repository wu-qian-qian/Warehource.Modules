using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plc.Domain.S7;
using Plc.Infrastructure.Database;

namespace Plc.Infrastructure.db;

public class NetConfiguration : IEntityTypeConfiguration<S7NetConfig>
{
    public void Configure(EntityTypeBuilder<S7NetConfig> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(S7NetConfig));
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.Property(p => p.ReadHeart).HasMaxLength(20);
        builder.Property(p => p.WriteHeart).HasMaxLength(20);
        builder.HasMany(e => e.S7EntityItems)
            .WithOne()
            .HasForeignKey(e => e.NetGuid);
    }
}