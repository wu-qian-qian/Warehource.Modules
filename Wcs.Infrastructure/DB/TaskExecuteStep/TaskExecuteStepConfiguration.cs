using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.TaskExecuteStep;

public class TaskExecuteStepConfiguration : IEntityTypeConfiguration<Domain.TaskExecuteStep.TaskExecuteStep>
{
    public void Configure(EntityTypeBuilder<Domain.TaskExecuteStep.TaskExecuteStep> builder)
    {
        builder.ToTable(Schemas.TableSchema + nameof(Domain.TaskExecuteStep.TaskExecuteStep));
        builder.HasKey(t => t.Id).IsClustered(false);
        builder.Property(t => t.Description)
            .HasMaxLength(50);
        builder.Property(t => t.PathNodeGroup)
            .HasMaxLength(50);
        builder.Property<byte[]>("Version").IsRowVersion();
    }
}