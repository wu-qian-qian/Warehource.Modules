namespace Common.Domain.EF;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetAsync(Guid id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task InserAsync(T entity);
    Task<List<T>> GetListAsync();

    /// <summary>
    ///     默认关闭状态追踪
    /// </summary>
    /// <param name="asNoTrack"></param>
    /// <returns></returns>
    Task<IQueryable<T>> GetQueryableAsync(bool asNoTrack = true);
}