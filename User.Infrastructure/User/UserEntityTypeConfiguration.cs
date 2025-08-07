using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.User;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<Domain.User>
{
    public void Configure(EntityTypeBuilder<Domain.User> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.User));
        builder.HasKey(u => u.Id).IsClustered(false);
        builder.HasIndex(p => p.Username).IsClustered(false);
        builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(p => p.RoleId).IsRequired();
        builder.HasOne(t => t.Role)
            .WithMany()
            .HasForeignKey(t => t.RoleId);
        builder.Property(p => p.LastModifierUser).HasMaxLength(20);
    }
}