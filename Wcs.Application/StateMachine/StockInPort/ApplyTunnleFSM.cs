using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using Serilog;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.DomainEvent.ApplyTunnle;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.StokInPort;

/// <summary>
///     入库口申请通道状态机
/// </summary>
/// <param name="_controller"></param>
/// <param name="_bus"></param>
/// <param name="_cacheService"></param>
[StateAttrubite($"{nameof(DeviceTypeEnum.StockPortIn)}-{nameof(TaskExecuteStepTypeEnum.ApplyTunnle)}")]
internal class ApplyTunnleFSM(IStockPortInController _controller, IEventBus _bus, ICacheService _cacheService)
    : IStateMachine
{
    public async ValueTask HandlerAsync(string json, CancellationToken token = default)
    {
        var wcsTask = _controller.GetWcsTaskByDeviceName(json);
        if (wcsTask.TaskExecuteStep.TaskExecuteStepType != TaskExecuteStepTypeEnum.ApplyTunnle)
        {
            Log.Logger
                .BusinessInformation(LogCategory.Business, new BusinessLog(json,
                    wcsTask.SerialNumber,
                    "状态机处理状态错误，无法进行业务逻辑处理"));
        }
        else
        {
            var EndPosition =
                await _bus.PublishAsync(new ApplyTunnleEvent { RegoinCode = wcsTask.RegionCode }, token);
            if (wcsTask.EndPosition != string.Empty)
            {
                wcsTask.StartPosition = _controller.GetPiplineByDeviceName(json);
                wcsTask.EndPosition = EndPosition;
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendPlc;
                await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(json), wcsTask);
                Log.Logger
                    .BusinessInformation(LogCategory.Business, new BusinessLog(json,
                        wcsTask.SerialNumber,
                        ""));
            }
        }
    }
}