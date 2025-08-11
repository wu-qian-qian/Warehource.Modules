using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.PlcMap;

internal class PlcMapConfiguration : IEntityTypeConfiguration<Domain.Plc.PlcMap>
{
    public void Configure(EntityTypeBuilder<Domain.Plc.PlcMap> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(PlcMap));
        builder.Property(p => p.DeviceName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.PlcName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.Description).HasMaxLength(64);
        builder.Property(p => p.LastModifierUser).HasMaxLength(32);
    }
}