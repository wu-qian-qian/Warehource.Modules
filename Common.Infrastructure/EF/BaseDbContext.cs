using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF;

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