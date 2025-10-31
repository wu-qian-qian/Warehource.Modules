using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.ExecuteNode;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.GetNextExecuteNode;

internal class GetNextExecuteNodeQueryHandler(IExecuteNodeRepository _executeNodeRepository)
    : IQueryHandler<GetNextExecuteNodeQuery, Result<DeviceTypeEnum>>
{
    public Task<Result<DeviceTypeEnum>> Handle(GetNextExecuteNodeQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<DeviceTypeEnum>();
        var data = _executeNodeRepository.GetQuerys()
            .Where(x => x.PahtNodeGroup == request.PathNodeGroup)
            .Where(x => x.TaskType == request.WcsTaskType)
            .Where(x => request.RegionCode.Contains(x.Region.Code))
            .ToArray();
        var currentNode = data.FirstOrDefault(p => p.CurrentDeviceType == request.DeviceType);
        if (currentNode == null)
        {
            result.SetMessage("未找到对应的执行节点");
        }
        else
        {
            if (currentNode.Index == 1)
            {
            }
            else
            {
                var nextNode = data.FirstOrDefault(p => p.Index == currentNode.Index + 1);
                if (nextNode == null)
                    // 没有下一个节点，说明是最后一个节点 TODO
                    result.SetValue(currentNode.CurrentDeviceType);
                else
                    result.SetValue(nextNode.CurrentDeviceType);
            }
        }

        return Task.FromResult(result);
    }
}