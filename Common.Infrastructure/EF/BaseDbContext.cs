using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF;

/// <summary>
///     优化项  AutoDetectChangesEnabled 属性  手动控制状态跟踪
/// </summary>
public abstract class BaseDbContext : DbContext
{
    private readonly LastModificationInterceptor _lastModificationInterceptor;

    public BaseDbContext(DbContextOptions options, LastModificationInterceptor lastModificationInterceptor) :
        base(options)
    {
        _lastModificationInterceptor = lastModificationInterceptor ??
                                       throw new ArgumentNullException(nameof(lastModificationInterceptor));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //��ע����
        optionsBuilder.AddInterceptors(_lastModificationInterceptor);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        return result;
    }
}