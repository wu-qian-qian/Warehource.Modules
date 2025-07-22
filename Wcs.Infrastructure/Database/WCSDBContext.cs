using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Wcs.Application.Abstract;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.JobConfigs;
using Wcs.Domain.Region;
using Wcs.Domain.S7;
using Wcs.Domain.Task;
using Wcs.Domain.TaskExecuteStep;
using Wcs.Infrastructure.DB.ExecuteNodePath;
using Wcs.Infrastructure.DB.JobConfig;
using Wcs.Infrastructure.DB.Region;
using Wcs.Infrastructure.DB.S7NetConfig;
using Wcs.Infrastructure.DB.TaskExecuteStep;
using Wcs.Infrastructure.DB.WcsTask;

namespace Wcs.Infrastructure.Database;

//Add-Migration InitialCreate -OutputDir "Database/Migrations" -Context WCSDBContext
public sealed class WCSDBContext : BaseDbContext, IUnitOfWork
{
    public WCSDBContext(DbContextOptions<WCSDBContext> options, LastModificationInterceptor lastModificationInterceptor)
        : base(options, lastModificationInterceptor)
    {
    }

    public DbSet<S7EntityItem> S7Entities { get; }

    public DbSet<ExecuteNodePath> ExecuteNodes { get; }

    public DbSet<S7NetConfig> Nets { get; }

    public DbSet<Region> Regions { get; }

    public DbSet<TaskExecuteStep> TaskExecuteSteps { get; }

    public DbSet<WcsTask> WcsTasks { get; }

    public DbSet<JobConfig> JobConfigs { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RegionConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfigConfiguration());
        modelBuilder.ApplyConfiguration(new EntityItemConfiguration());
        modelBuilder.ApplyConfiguration(new ExecuteNodePathConfiguration());
        modelBuilder.ApplyConfiguration(new NetConfiguration());
        modelBuilder.ApplyConfiguration(new TaskExecuteStepConfiguration());
        modelBuilder.ApplyConfiguration(new WcsTaskConfiguration());
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}