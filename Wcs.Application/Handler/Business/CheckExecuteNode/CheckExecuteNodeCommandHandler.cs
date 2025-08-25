using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.Business.CheckExecuteNode;

public class CheckExecuteNodeCommandHandler(
    IExecuteNodeRepository _executeNodeRepository,
    IRegionRepository _regionRepository)
    : ICommandHandler<CheckExecuteNodeCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(CheckExecuteNodeCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = new();
        var wcsTask = request.WcsTask;
        var region = _regionRepository.Get(wcsTask.RegionId.Value);
        if (region != null)
        {
            //设备的区域组保函该区域
            if (request.DeviceRegionCode.Contains(region.Code))
            {
                var executeNodePath = _executeNodeRepository.GetQuerys()
                    .Where(p => p.PahtNodeGroup == wcsTask.TaskExecuteStep.PathNodeGroup).ToArray();
                if (executeNodePath.All(p => p.Region.Id == wcsTask.RegionId))
                {
                    var index = executeNodePath.First(p => p.CurrentDeviceType == wcsTask.TaskExecuteStep.DeviceType)
                        .Index;
                    var nextType = index++;
                    var executeNode = executeNodePath.FirstOrDefault(p => p.Index == nextType);
                    if (executeNode != null)
                    {
                        wcsTask.TaskExecuteStep.DeviceType = executeNode.CurrentDeviceType;
                        result.SetValue(true);
                    }
                    else
                    {
                        result.SetValue(false);
                        //任务完结
                    }
                }
                else
                {
                    result.SetMessage("任务执行的区域与路径区域不符合");
                }
            }
            else
            {
                result.SetMessage("区域信息错误无法执行");
            }
        }
        else
        {
            result.SetMessage("区域信息未获取到");
        }

        return Task.FromResult(result);
    }
}