using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Role;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Role> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.Role));
        builder.HasKey(builder => builder.Id).IsClustered(false);
        builder.HasIndex(p => p.RoleName).IsClustered(false);
        builder.Property(p => p.RoleName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.LastModifierUser).HasMaxLength(20);
    }
}