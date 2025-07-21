using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Infrastructure.Database;

namespace User.Infrastructure.Role;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Role> builder)
    {
        builder.ToTable(nameof(Domain.Role),Schemas.TableSchema);
        builder.HasKey(builder => builder.Id).IsClustered(false);
        builder.HasIndex(p => p.RoleName).IsClustered(false);
        builder.Property(p => p.RoleName)
            .IsRequired()
            .HasMaxLength(50);
    }
}