using Common.Application.Caching;
using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.GetExecuteTask;

internal class GetExecuteTaskQueryHandler(
    IWcsTaskRepository _taskRepository,
    ICacheService _cacheService) : IQueryHandler<GetExecuteTaskQuery, WcsTask[]>
{
    public async Task<WcsTask[]> Handle(GetExecuteTaskQuery request, CancellationToken cancellationToken)
    {
        var wcsTaskArray = default(WcsTask[]);
        var cacheTask = await _cacheService.GetAsync<WcsTask>(request.TaskCacheKey, cancellationToken);
        if (cacheTask != null)
        {
            if (cacheTask.TaskExecuteStep.CurentDevice == request.DeviceName ||
                cacheTask.TaskExecuteStep.DeviceType == request.DeviceType)
                wcsTaskArray = new[] { cacheTask };
        }
        else
        {
            wcsTaskArray = _taskRepository.GetWcsTaskQuerys()
                .Where(p => p.TaskExecuteStep.DeviceType == request.DeviceType ||
                            p.TaskExecuteStep.CurentDevice == request.DeviceName)
                .Where(p => p.RegionCode == null || request.Region.Contains(p.RegionCode))
                .Where(p => p.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.None)
                .WhereIf(request.WcsTaskType.HasValue, p => p.TaskType == request.WcsTaskType)
                .WhereIf(request.TaskStatus.HasValue, p => p.TaskStatus == request.TaskStatus)
                .WhereIf(request.SerialNumber.HasValue, p => p.SerialNumber == request.SerialNumber)
                .WhereIf(string.IsNullOrEmpty(request.Container) == false, p => p.Container == request.Container)
                .WhereIf(string.IsNullOrEmpty(request.PutTunnle) == false,
                    p => p.PutLocation.PutTunnel == request.PutTunnle)
                .WhereIf(string.IsNullOrEmpty(request.GetTunnle) == false,
                    p => p.GetLocation.GetTunnel == request.GetTunnle)
                .OrderBy(p => p.Level).ThenBy(p => p.CreationTime)
                .ToArray();
        }

        return wcsTaskArray;
    }
}