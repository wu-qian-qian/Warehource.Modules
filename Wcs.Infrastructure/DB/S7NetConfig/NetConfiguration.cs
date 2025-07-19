using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.S7NetConfig;

public class NetConfiguration : IEntityTypeConfiguration<Domain.S7.S7NetConfig>
{
    public void Configure(EntityTypeBuilder<Domain.S7.S7NetConfig> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.S7.S7NetConfig));
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.HasMany(e => e.S7EntityItems)
            .WithOne()
            .HasForeignKey(e => e.NetGuid);
    }
}