using Common.Domain.EF;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF.Repository;

public class EfCoreRepository<T, TDbContext> : IEfCoreRepository<T> where T :
    IEntity
    where TDbContext : DbContext
{
    public EfCoreRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected TDbContext DbContext { get; init; }

    public virtual Task<T?> GetAsync(Guid id)
    {
        T entity = default;
        if (DbContext.Set<T>().Any(p => p.Id == id)) entity = DbContext.Set<T>().First(p => p.Id == id);

        return Task.FromResult(entity);
    }

    public virtual Task UpdateAsync(T entity)
    {
        DbContext.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity)
    {
        DbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task InserAsync(T entity)
    {
        await DbContext.AddAsync(entity);
    }

    public virtual async Task<List<T>> GetListAsync()
    {
        return await DbContext.Query<T>().ToListAsync();
    }

    public virtual Task<IQueryable<T>> GetQueryableAsync(bool asNoTrack = true)
    {
        if (asNoTrack)
        {
            var queryable = DbContext.Query<T>();
            return Task.FromResult(queryable);
        }
        else
        {
            var queryable = DbContext.Set<T>().AsQueryable();
            return Task.FromResult(queryable);
        }
    }
}