using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Application.StateMachine;
using Common.Shared;
using Common.Shared.Log;
using Serilog;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute;

internal abstract class DeviceCommandHandler<T>(IStateMachineManager _statemachineManager)
    : ICommandHandler<T> where T : IExecuteDeviceCommand
{
    public virtual async Task Handle(T request, CancellationToken cancellationToken)
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
                var key = $"{device.DeviceType}-{wcsTask.TaskExecuteStep.TaskExecuteStepType}";
                var data = device.Name;
                await _statemachineManager.NextStatus(key, data, cancellationToken);
            }
            else if (wcsTask == null)
            {
                var key = $"{device.DeviceType}-{TaskExecuteStepTypeEnum.GetWcsTask}";
                var data = device.Name;
                await _statemachineManager.NextStatus(key, data, cancellationToken);
            }
            else
            {
                //区域不匹配，清除任务
                Log.Logger.BusinessInformation(LogCategory.Business,
                    new BusinessLog(device.Name, wcsTask.SerialNumber,
                        $"设备区域:{device.RegionCodes}与任务区域:{wcsTask.RegionCode}不匹配，清除任务"));
                device.ClearWcsTask();
            }
        }
    }
}