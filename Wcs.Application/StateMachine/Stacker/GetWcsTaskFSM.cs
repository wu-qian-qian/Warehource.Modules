using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using MediatR;
using Serilog;
using Wcs.Application.DeviceController.Stacker;
using Wcs.Application.DomainHandler.GetTranshipInTaskNo;
using Wcs.Application.Handler.Business.GetExecuteTask;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.Stacker;

/// <summary>
///     堆垛机获取WCS任务状态机
/// </summary>
/// <param name="_controller"></param>
/// <param name="_sender"></param>
/// <param name="_cacheService"></param>
[StateAttrubite($"{nameof(DeviceTypeEnum.Stacker)}-{nameof(TaskExecuteStepTypeEnum.GetWcsTask)}")]
internal class GetWcsTaskFSM(
    IStackerController _controller,
    ISender _sender,
    ICacheService _cacheService,
    IEventBus _bus) : IStateMachine
{
    public async ValueTask HandlerAsync(string json, CancellationToken token = default)
    {
        var isTranshipIn = _controller.IsTranshipPointByDeviceName(json);
        var wcsTask = default(WcsTask);
        if (isTranshipIn)
        {
            wcsTask = await GetStockInTaskAsync(json);
        }
        else
        {
            wcsTask = await GetStockMoveTaskAsync(json);
            if (wcsTask == null) wcsTask = await GetStockOutTaskAsync(json);

            if (wcsTask == null) wcsTask = await GetStockInTaskAsync(json);
        }

        if (wcsTask != null)
        {
            wcsTask.TaskExecuteStep.CurentDevice = json;
            wcsTask.TaskExecuteStep.DeviceType = DeviceTypeEnum.Stacker;
            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.GetStationPoint;
            //缓存任务
            await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(json), wcsTask, TimeSpan.FromMinutes(20),
                token);
            _controller.SetWcsTask(json, wcsTask);
            Log.Logger
                .BusinessInformation(LogCategory.Business,
                    new BusinessLog(json, wcsTask.SerialNumber, "获取到堆垛机执行任务"));
        }
    }

    #region 获取到WCS任务

    public async Task<WcsTask> GetStockInTaskAsync(string deviceName)
    {
        var tunnle = _controller.GetTunnleByDeviceName(deviceName);
        var wcsTask = default(WcsTask);
        //堆垛机此时位置在入库口位置
        var taskNo = await _bus.PublishAsync(new GetTranshipInTaskNoEvent { Tunnle = tunnle });
        if (string.IsNullOrEmpty(taskNo))
            Log.Logger
                .BusinessInformation(LogCategory.Business,
                    new BusinessLog(deviceName, -9999, "输送任务为空无法获取执行任务"));

        var serialNumber = int.Parse(taskNo);
        var wcsTasks = await _sender.Send(new GetExecuteTaskQuery
        {
            SerialNumber = serialNumber
        });
        if (wcsTasks.Any() == false)
            Log.Logger
                .BusinessInformation(LogCategory.Business,
                    new BusinessLog(deviceName, serialNumber, "输送任务为空无法获取执行任务"));

        wcsTask = wcsTasks.First();
        return wcsTask;
    }

    /// <summary>
    ///     获取出库任务
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public async Task<WcsTask> GetStockOutTaskAsync(string deviceName)
    {
        var tunnle = _controller.GetTunnleByDeviceName(deviceName);
        var wcsTask = default(WcsTask);
        var wcsTasks = await _sender.Send(new GetExecuteTaskQuery
        {
            DeviceName = deviceName,
            DeviceType = DeviceTypeEnum.Stacker,
            TaskStatus = WcsTaskStatusEnum.Analysited,
            Region = _controller.GetRegionCodesByDeviceName(deviceName),
            TaskCacheKey = _controller.GetWcsTaskCacheOfKey(deviceName),
            WcsTaskType = WcsTaskTypeEnum.StockOut,
            GetTunnle = tunnle
        });
        if (wcsTasks.Any()) wcsTask = wcsTasks.First();

        return wcsTask;
    }

    /// <summary>
    ///     获取移库任务
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public async Task<WcsTask> GetStockMoveTaskAsync(string deviceName)
    {
        var tunnle = _controller.GetTunnleByDeviceName(deviceName);
        var wcsTask = default(WcsTask);
        var wcsTasks = await _sender.Send(new GetExecuteTaskQuery
        {
            DeviceName = deviceName,
            DeviceType = DeviceTypeEnum.Stacker,
            TaskStatus = WcsTaskStatusEnum.Analysited,
            Region = _controller.GetRegionCodesByDeviceName(deviceName),
            TaskCacheKey = _controller.GetWcsTaskCacheOfKey(deviceName),
            WcsTaskType = WcsTaskTypeEnum.StockMove,
            GetTunnle = tunnle
        });
        if (wcsTasks.Any()) wcsTask = wcsTasks.First();

        return wcsTask;
    }

    #endregion
}