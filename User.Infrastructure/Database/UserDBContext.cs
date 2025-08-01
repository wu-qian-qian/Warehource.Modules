using Common.Infrastructure.EF;
using Identity.Application.Abstract;
using Identity.Infrastructure.Role;
using Identity.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Database;

// Add-Migration InitialCreate -OutputDir "Database/Migrations" -Context UserDBContext
public class UserDBContext : BaseDbContext, IUnitOfWork
{
    public UserDBContext(DbContextOptions<UserDBContext> options,
        LastModificationInterceptor lastModificationInterceptor) : base(options, lastModificationInterceptor)
    {
    }

    public DbSet<Domain.User> Users { get; set; }

    public DbSet<Domain.Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
      
    }
}