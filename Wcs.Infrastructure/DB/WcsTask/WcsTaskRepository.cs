using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Wcs.Domain.Task;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.WcsTask;

public class WcsTaskRepository(WCSDBContext _db) : IWcsTaskRepository
{
    public void Insert(IEnumerable<Domain.Task.WcsTask> tasks)
    {
        if (tasks.Count() > 100)
        {
            //更新也一样只触发一次状态跟踪
            _db.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                // 添加大量实体，此时不会自动触发变更检测
                _db.WcsTasks.AddRange(tasks);

                // 手动触发一次变更检测（仅一次）
                _db.ChangeTracker.DetectChanges();
            }
            finally
            {
                _db.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
        else
        {
            _db.WcsTasks.AddRange(tasks);
        }
    }

    public Domain.Task.WcsTask Get(Guid id)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (_db.WcsTasks.Any(p => p.Id == id))
            wcsTask = _db.WcsTasks.Include(p => p.TaskExecuteStep).First(p => p.Id == id);
        return wcsTask;
    }

    public Domain.Task.WcsTask Get(string taskCode)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (_db.WcsTasks.Any(p => p.TaskCode == taskCode))
            wcsTask = _db.WcsTasks.Include(p => p.TaskExecuteStep).First(p => p.TaskCode == taskCode);
        return wcsTask;
    }

    public Domain.Task.WcsTask Get(int serialNumber)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (_db.WcsTasks.Any(p => p.SerialNumber == serialNumber))
            wcsTask = _db.WcsTasks.Include(p => p.TaskExecuteStep).First(p => p.SerialNumber == serialNumber);
        return wcsTask;
    }

    public IQueryable<Domain.Task.WcsTask> GetWcsTaskQuerys()
    {
        return _db.Query<Domain.Task.WcsTask>().Include(p => p.TaskExecuteStep);
    }

    public void Update(Domain.Task.WcsTask wcsTask)
    {
        _db.WcsTasks.Update(wcsTask);
    }

    public void Updates(IEnumerable<Domain.Task.WcsTask> tasks)
    {
        _db.ChangeTracker.AutoDetectChangesEnabled = false;
        try
        {
            // 添加大量实体，此时不会自动触发变更检测
            _db.WcsTasks.UpdateRange(tasks);

            // 手动触发一次变更检测（仅一次）
            _db.ChangeTracker.DetectChanges();
        }
        finally
        {
            _db.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }
}