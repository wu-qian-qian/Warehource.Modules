using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Plc.Application.Abstract;
using Plc.Domain.S7;
using Plc.Infrastructure.db;

namespace Plc.Infrastructure.Database;

//Add-Migration InitialCreate -OutputDir "Database/Migrations" -Context PlcDBContext
public sealed class PlcDBContext : BaseDbContext, IUnitOfWork
{
    public PlcDBContext(DbContextOptions<PlcDBContext> options, LastModificationInterceptor lastModificationInterceptor)
        : base(options, lastModificationInterceptor)
    {
    }

    public DbSet<S7EntityItem> S7Entities { get;protected set; }


    public DbSet<S7NetConfig> Nets { get; protected set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new NetConfiguration());
        modelBuilder.ApplyConfiguration(new EntityItemConfiguration());
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}