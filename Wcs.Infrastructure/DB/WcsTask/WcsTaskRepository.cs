using Common.Infrastructure.EF;
using Wcs.Domain.Task;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.WcsTask;

public class WcsTaskRepository(WCSDBContext _db) : IWcsTaskRepository
{
    public void Insert(IEnumerable<Domain.Task.WcsTask> tasks)
    {
        _db.WcsTasks.AddRange(tasks);
    }

    public Domain.Task.WcsTask Get(Guid id)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (_db.WcsTasks.Any(p => p.Id == id)) wcsTask = _db.WcsTasks.First(p => p.Id == id);
        return wcsTask;
    }

    public Domain.Task.WcsTask Get(string taskCode)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (_db.WcsTasks.Any(p => p.TaskCode == taskCode))
            wcsTask = _db.WcsTasks.First(p => p.TaskCode == taskCode);
        return wcsTask;
    }

    public Domain.Task.WcsTask Get(int serialNumber)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (_db.WcsTasks.Any(p => p.SerialNumber == serialNumber)) 
            wcsTask = _db.WcsTasks.First(p => p.SerialNumber == serialNumber);
        return wcsTask;
    }

    public IQueryable<Domain.Task.WcsTask> GetWcsTaskQuerys()
    {
        return _db.Query<Domain.Task.WcsTask>();
    }

    public void Update(Domain.Task.WcsTask wcsTask)
    {
        _db.WcsTasks.Update(wcsTask);
    }
}