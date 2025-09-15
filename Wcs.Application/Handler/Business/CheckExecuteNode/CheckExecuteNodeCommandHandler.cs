using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using MediatR;
using Wcs.Application.Handler.Business.SetExecuteDevice;
using Wcs.Application.Handler.Http.Complate;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Region;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.CheckExecuteNode;

public class CheckExecuteNodeCommandHandler(
    ISender _sender,
    IExecuteNodeRepository _executeNodeRepository,
    IRegionRepository _regionRepository)
    : ICommandHandler<CheckExecuteNodeCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CheckExecuteNodeCommand request, CancellationToken cancellationToken)
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
                    if (index == 1) wcsTask.TaskStatus = WcsTaskStatusEnum.InProgress;

                    var nextType = index++;
                    var executeNode = executeNodePath.FirstOrDefault(p => p.Index == nextType);
                    if (executeNode != null)
                    {
                        wcsTask.TaskExecuteStep.DeviceType = executeNode.CurrentDeviceType;
                        if (request.IsGetDeviceName)
                        {
                            var deviceName = await _sender
                                .Send(new SetExecuteDeviceCommand
                                {
                                    DeviceType = wcsTask.TaskExecuteStep.DeviceType.Value,
                                    Title = request.Title
                                });
                            wcsTask.TaskExecuteStep.CurentDevice = deviceName;
                        }

                        result.SetValue(true);
                    }
                    else
                    {
                        result.SetValue(false);
                        wcsTask.TaskStatus = WcsTaskStatusEnum.Completed;
                        await _sender.Send(new ComplateCommand
                        {
                            WcsTask = request.WcsTask
                        });
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

        return result;
    }
}