using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using Serilog;
using Wcs.Application.DeviceController.Stacker;
using Wcs.Application.DomainHandler.GetTranshipOutPos;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.Stacker;

[StateAttrubite($"{nameof(DeviceTypeEnum.Stacker)}-{nameof(TaskExecuteStepTypeEnum.GetStationPoint)}")]
internal class GetStationFSM(IStackerController _controller, ICacheService _cache, IEventBus _bus) : IStateMachine
{
    public async ValueTask HandlerAsync(string deviceName, CancellationToken token = default)
    {
        var wcsTask = _controller.GetWcsTaskByDeviceName(deviceName);
        if (wcsTask.TaskType != WcsTaskTypeEnum.StockIn)
        {
            var putlocation = await _bus.PublishAsync(new GetTranshipOutPosEvent
            {
                Tunnle = wcsTask.GetLocation.GetTunnel
            }, token);
            wcsTask.PutLocation = putlocation;
        }

        if (wcsTask.PutLocation.IsValid() && wcsTask.GetLocation.IsValid())
        {
            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendPlc;
            await _cache.SetAsync(_controller.GetWcsTaskCacheOfKey(deviceName), wcsTask);
            Log.Logger
                .BusinessInformation(LogCategory.Business,
                    new BusinessLog(deviceName, wcsTask.SerialNumber, "堆垛机取放货站台更新成功切换状态"));
        }
        else
        {
            Log.Logger
                .BusinessInformation(LogCategory.Business,
                    new BusinessLog(deviceName, wcsTask.SerialNumber, "堆垛机取放货站台存在异常"));
        }
    }
}