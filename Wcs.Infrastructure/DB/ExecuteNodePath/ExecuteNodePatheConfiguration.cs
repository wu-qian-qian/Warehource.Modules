using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.ExecuteNodePath;

public class ExecuteNodePatheConfiguration : IEntityTypeConfiguration<Domain.ExecuteNode.ExecuteNodePath>
{
    public void Configure(EntityTypeBuilder<Domain.ExecuteNode.ExecuteNodePath> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.ExecuteNode.ExecuteNodePath));
        builder.HasKey(t => t.Id).IsClustered(false);
        builder.Property(p => p.LastModifierUser).HasMaxLength(20);
        builder.HasOne(p => p.Region).WithMany().HasForeignKey(P => P.RegionId);
    }
}