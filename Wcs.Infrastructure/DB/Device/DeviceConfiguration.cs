using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.Device;

public class DeviceConfiguration : IEntityTypeConfiguration<Domain.Device.Device>
{
    public void Configure(EntityTypeBuilder<Domain.Device.Device> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.Device.Device));
        builder.HasKey(t => t.Id).IsClustered(false);
        builder.HasIndex(t => t.DeviceName)
            .IsUnique();
    }
}