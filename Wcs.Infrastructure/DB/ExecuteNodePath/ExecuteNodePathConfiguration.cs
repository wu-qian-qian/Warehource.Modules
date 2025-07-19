using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.ExecuteNodePath;

public class ExecuteNodePathConfiguration : IEntityTypeConfiguration<Domain.ExecuteNode.ExecuteNodePath>
{
    public void Configure(EntityTypeBuilder<Domain.ExecuteNode.ExecuteNodePath> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.ExecuteNode.ExecuteNodePath));
        builder.HasKey(t => t.Id).IsClustered(false);
        builder.Property(t => t.CurrentDeviceName)
            .HasMaxLength(20);
        builder.HasOne(b => b.Region)
            .WithOne()
            .HasForeignKey<Domain.ExecuteNode.ExecuteNodePath>(b => b.RegionId);
    }
}