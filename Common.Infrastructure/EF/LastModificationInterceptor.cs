using Common.Domain.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.EF;

public class LastModificationInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LastModificationInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            var user = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Permission")?.Value;
            eventData.Context.ChangeTracker.Entries<ILastModification>()
                .Where(e => e.State == EntityState.Modified).ToList()
                .ForEach(entry => { entry.Entity.SetLastModification(user); });
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            //把被软删除的对象从Cache删除，否则FindAsync()还能根据Id获取到这条数据
            //因为FindAsync如果能从本地Cache找到，就不会去数据库上查询，而从本地Cache找的过程中不会管QueryFilter
            //就会造成已经软删除的数据仍然能够通过FindAsync查到的问题，因此这里把对应跟踪对象的state改为Detached，就会从缓存中删除了
            var softDeletedEntities = eventData.Context.ChangeTracker.Entries<ISoftDelete>()
                .Where(e => e.State == EntityState.Modified && e.Entity.IsDeleted)
                .Select(e => e.Entity).ToList();

            softDeletedEntities.ForEach(e => eventData.Context.Entry(e).State = EntityState.Detached);
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}