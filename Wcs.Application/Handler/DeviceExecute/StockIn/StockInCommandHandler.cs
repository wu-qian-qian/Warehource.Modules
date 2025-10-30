using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Application.StateMachine;
using Common.Helper;
using Common.JsonExtension;
using Common.Shared;
using Common.Shared.Log;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.GetExecuteTask;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.CustomEvents.Saga;
using Wcs.Device.DeviceStructure.StockPort;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute.StockIn;

/// <summary>
/// </summary>
/// <param name="_taskRepository"></param>
/// <param name="_cacheService"></param>
internal class StockInCommandHandler(
    IStateMachineManager _statemachineManager)
    : ICommandHandler<StockInCommand>
{
    public async Task Handle(StockInCommand request, CancellationToken cancellationToken)
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