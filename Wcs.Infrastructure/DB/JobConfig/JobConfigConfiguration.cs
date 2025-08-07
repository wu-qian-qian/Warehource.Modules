using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.JobConfig;

internal class JobConfigConfiguration : IEntityTypeConfiguration<Domain.JobConfigs.JobConfig>
{
    public void Configure(EntityTypeBuilder<Domain.JobConfigs.JobConfig> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.JobConfigs.JobConfig));
        builder.HasKey(t => t.Id).IsClustered(false);
        builder.HasIndex(t => t.Name).IsUnique();
        builder.Ignore(p => p.TimeoutSeconds);
        builder.Property(p => p.JobType).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(builder => builder.Description)
            .IsRequired(false)
            .HasMaxLength(50);
        builder.Property(p => p.LastModifierUser).HasMaxLength(20);
    }
}