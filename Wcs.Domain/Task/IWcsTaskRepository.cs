namespace Wcs.Domain.Task;

public interface IWcsTaskRepository
{
    void Insert(IEnumerable<WcsTask> tasks);

    WcsTask Get(Guid id);

    WcsTask Get(string taskCode);

    IQueryable<WcsTask> GetWcsTaskQuerys();

    void Update(WcsTask wcsTask);
}