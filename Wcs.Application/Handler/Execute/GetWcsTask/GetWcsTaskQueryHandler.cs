using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Execute.GetWcsTask;

/// <summary>
///     获取当前设备的执行任务
/// </summary>
/// <param name="_executeNodeRepository"></param>
/// <param name="_taskRepository"></param>
public class GetWcsTaskQueryHandler(
    IWcsTaskRepository _taskRepository)
    : IQueryHandler<GetWcsTaskQuery, IEnumerable<WcsTask>>
{
    public Task<IEnumerable<WcsTask>> Handle(GetWcsTaskQuery request, CancellationToken cancellationToken)
    {
        //只获取
        IEnumerable<WcsTask> wcstasks = _taskRepository.GetWcsTaskQuerys()
            .Where(p => p.TaskStatus == WcsTaskStatusEnum.Analysited || WcsTaskStatusEnum.InProgress == p.TaskStatus)
            .WhereIf(request.DeviceType != null, p => p.TaskExecuteStep.DeviceType == request.DeviceType)
            .WhereIf(request.DeviceName != null, p => p.TaskExecuteStep.CurentDevice == request.DeviceName)
            .WhereIf(request.DeviceName != null, p => p.TaskExecuteStep.IsSend == false)
            .ToArray();

        return Task.FromResult(wcstasks);
    }
}