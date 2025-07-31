using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Wcs.Application.Abstract;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.JobConfigs;
using Wcs.Domain.Region;
using Wcs.Domain.Task;
using Wcs.Domain.TaskExecuteStep;
using Wcs.Infrastructure.DB.Device;
using Wcs.Infrastructure.DB.ExecuteNodePath;
using Wcs.Infrastructure.DB.JobConfig;
using Wcs.Infrastructure.DB.Region;
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


    public DbSet<ExecuteNodePath> ExecuteNodes { get; set; }


    public DbSet<Region> Regions { get; set; }

    public DbSet<TaskExecuteStep> TaskExecuteSteps { get; set; }

    public DbSet<WcsTask> WcsTasks { get; set; }

    public DbSet<JobConfig> JobConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RegionConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfigConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new TaskExecuteStepConfiguration());
        modelBuilder.ApplyConfiguration(new WcsTaskConfiguration());
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}