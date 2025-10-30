using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Application.StateMachine;
using Common.Shared;
using Common.Shared.Log;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.CustomEvents.Saga;
using Wcs.Device.DeviceStructure.Stacker;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute.Stacker;

/// <summary>
///     堆垛机业务处理事件
/// </summary>
/// <param name="_sender"></param>
internal class StackerDeviceCommandHandler(
    ISender _sender,
    IPublishEndpoint _publishEndpoint,
    ICacheService _cacheService,
    IStateMachineManager _statemachineManager,
    IAnalysisLocation _locationService)
    : ICommandHandler<StackerDeviceCommand>
{
    public async Task Handle(StackerDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = request.Device;
        var wcsTask = device.WcsTask;
        if (wcsTask?.TaskExecuteStep.CurentDevice != device.Name)
        {
            //设备不匹配，清除任务
            wcsTask = null;
            device.ClearWcsTask();
        }

        if (device.CanExecute())
        {
            //存在任务，且输送可以执行
            if (wcsTask != null && device.CanRegionExecute(wcsTask.RegionCode))
            {
                string key = $"{device.DeviceType}-{wcsTask.TaskExecuteStep.TaskExecuteStepType}";
                string data = device.Name;
                await _statemachineManager.NextStatus(key, data, cancellationToken);
            }
            else if (wcsTask == null)
            {
                string key = $"{device.DeviceType}-{TaskExecuteStepTypeEnum.GetWcsTask}";
                string data = device.Name;
                await _statemachineManager.NextStatus(key, data, cancellationToken);
            }
            else
            {
                //区域不匹配，清除任务
                Serilog.Log.Logger.BusinessInformation(LogCategory.Business,
                    new BusinessLog(device.Name, wcsTask.SerialNumber,
                        $"设备区域:{device.RegionCodes}与任务区域:{wcsTask.RegionCode}不匹配，清除任务"));
                device.ClearWcsTask();
            }
        }
    }
}