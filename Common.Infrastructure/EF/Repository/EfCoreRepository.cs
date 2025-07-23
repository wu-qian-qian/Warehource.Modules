using Common.Domain.EF;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF.Repository;

public class EfCoreRepository<T, TDbContext> : IEfCoreRepository<T> where T :
    IEntity
    where TDbContext : DbContext
{
    public EfCoreRepository(TDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected TDbContext dbContext { get; init; }

    public virtual async Task<T?> GetAsync(Guid id)
    {
        if (dbContext.Set<T>().Any(p => p.Id == id)) return dbContext.Set<T>().First(p => p.Id == id);
        return null;
    }

    public virtual Task UpdateAsync(T entity)
    {
        dbContext.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity)
    {
        dbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task InserAsync(T entity)
    {
        await dbContext.AddAsync(entity);
    }

    public virtual async Task<List<T>> GetListAsync()
    {
        return await dbContext.Query<T>().ToListAsync();
    }

    public virtual Task<IQueryable<T>> GetQueryableAsync(bool asNoTrack = true)
    {
        if (asNoTrack)
        {
            var queryable = dbContext.Query<T>();
            return Task.FromResult(queryable);
        }
        else
        {
            var queryable = dbContext.Set<T>().AsQueryable();
            return Task.FromResult(queryable);
        }
    }
}