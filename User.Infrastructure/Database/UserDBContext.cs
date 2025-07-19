using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using User.Application.Abstract;
using User.Infrastructure.Role;
using User.Infrastructure.User;

namespace User.Infrastructure.Database;

// Add-Migration InitialCreate -OutputDir "Data/Migrations" -Context UserDBContext
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
}