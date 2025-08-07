using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.Region;

public class RegionConfiguration : IEntityTypeConfiguration<Domain.Region.Region>
{
    public void Configure(EntityTypeBuilder<Domain.Region.Region> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.Region.Region));
        builder.HasKey(r => r.Id).IsClustered(false);
        builder.HasIndex(u => u.Code).IsUnique();
        builder.Property(r => r.Code)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(r => r.Description).HasMaxLength(50);
        builder.Property(p => p.LastModifierUser).HasMaxLength(20);
    }
}