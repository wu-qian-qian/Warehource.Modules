namespace Wcs.Domain.Task;

public interface IWcsTaskRepository
{
    void Insert(IEnumerable<WcsTask> tasks);

    WcsTask Get(Guid id);

    WcsTask Get(string taskCode);

    WcsTask Get(int serialNumber);

    IQueryable<WcsTask> GetWcsTaskQuerys();

    void Update(WcsTask wcsTask);
}