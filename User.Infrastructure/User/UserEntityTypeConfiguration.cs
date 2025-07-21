using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Infrastructure.Database;

namespace User.Infrastructure.User;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<Domain.User>
{
    public void Configure(EntityTypeBuilder<Domain.User> builder)
    {
        builder.ToTable(nameof(Domain.User), Schemas.TableSchema);
        builder .HasKey(u => u.Id).IsClustered(false);
        builder.HasIndex(p => p.Username).IsClustered(false);
        builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(p => p.RoleId).IsRequired();
        builder.HasOne(t => t.Role)
            .WithOne()
            .HasForeignKey<Domain.User>(t => t.RoleId);
    }
}