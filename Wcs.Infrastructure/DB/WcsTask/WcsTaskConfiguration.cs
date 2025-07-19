using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.WcsTask;

public class WcsTaskConfiguration : IEntityTypeConfiguration<Domain.Task.WcsTask>
{
    public void Configure(EntityTypeBuilder<Domain.Task.WcsTask> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.Task.WcsTask));
        builder.HasKey(t => t.Id).IsClustered(false);
        builder.HasIndex(p => p.SerialNumber).IsUnique();
        builder.HasIndex(p => p.TaskCode).IsUnique();
        builder.Property(t => t.TaskCode)
            .IsRequired(false)
            .HasMaxLength(50);
        builder.OwnsOne(t => t.GetLocation, g =>
        {
            g.Property(p => p.GetColumn).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.GetRow).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.GetFloor).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.GetTunnel).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.GetDepth).IsRequired(false).HasMaxLength(10);
        });
        builder.OwnsOne(t => t.PutLocation, g =>
        {
            g.Property(p => p.PutColumn).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.PutRow).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.PutFloor).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.PutTunnel).IsRequired(false).HasMaxLength(10);
            g.Property(p => p.PutDepth).IsRequired(false).HasMaxLength(10);
        });
        builder.HasOne(t => t.TaskExecuteStep)
            .WithOne()
            .HasForeignKey<Domain.TaskExecuteStep.TaskExecuteStep>(t => t.Id);

        builder.HasOne(a => a.TaskExecuteStep).WithMany()
            .HasForeignKey(a => a.TaskExecuteStepId);

        builder.Property<byte[]>("Version").IsRowVersion();
    }
}