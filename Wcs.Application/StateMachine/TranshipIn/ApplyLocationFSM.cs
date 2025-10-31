using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.DomainHandler.ApplyLocation;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.TranshipIn;

[StateAttrubite($"{nameof(DeviceTypeEnum.StackerInTranShip)}-{nameof(TaskExecuteStepTypeEnum.ApplyLocation)}")]
internal class ApplyLocationFSM(
    IStackerTranshipInController _controller,
    IAnalysisLocation _analysisLocation,
    IEventBus _bus,
    ICacheService _cacheService) : IStateMachine
{
    public async ValueTask HandlerAsync(string deviceName, CancellationToken token = default)
    {
        var wcsTask = _controller.GetWcsTaskByDeviceName(deviceName);
        var location = _controller.GetLocationByDeviceName(deviceName);
        var tunnle = _controller.GetTunnleByDeviceName(deviceName);
        var getLocatoin = _analysisLocation.AnalysisGetLocation(location);
        wcsTask.GetLocation = getLocatoin;
        var putLocation = await _bus.PublishAsync(new ApplyLocationEvent
        {
            Tunnle = tunnle
        }, token);
        wcsTask.PutLocation = putLocation;
        wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.Complate;
        await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(deviceName), wcsTask);
        Log.Logger.BusinessInformation(LogCategory.Business,
            new BusinessLog(deviceName, wcsTask.SerialNumber,
                "入库口堆垛机申请库位完成"));
    }
}