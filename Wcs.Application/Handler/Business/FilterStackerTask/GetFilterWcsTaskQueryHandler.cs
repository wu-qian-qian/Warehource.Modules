using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.FilterStackerTask;

public class GetFilterWcsTaskQueryHandler(
    IWcsTaskRepository _taskRepository)
    : IQueryHandler<GetFilterWcsTaskQuery, IEnumerable<WcsTask>>
{
    public Task<IEnumerable<WcsTask>> Handle(GetFilterWcsTaskQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<WcsTask> wcsTasks = _taskRepository.GetWcsTaskQuerys()
            .Where(p => p.TaskStatus == WcsTaskStatusEnum.Analysited || WcsTaskStatusEnum.InProgress == p.TaskStatus)
            .WhereIf(request.DeviceType != null, p => p.TaskExecuteStep.DeviceType == request.DeviceType)
            .WhereIf(request.DeviceName != null, p => p.TaskExecuteStep.CurentDevice == request.DeviceName)
            .ToArray();
        return Task.FromResult(wcsTasks);
    }
}