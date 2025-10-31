using Common.Application.Caching;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using MediatR;
using Serilog;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.Handler.Business.GetExecuteTask;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.StokInPort;

/// <summary>
///     入库口获取WCS任务状态机
/// </summary>
/// <param name="_controller"></param>
/// <param name="_sender"></param>
/// <param name="_cacheService"></param>
[StateAttrubite($"{nameof(DeviceTypeEnum.StockPortIn)}-{nameof(TaskExecuteStepTypeEnum.GetWcsTask)}")]
internal class GetWcsTaskFSM(IStockPortInController _controller, ISender _sender, ICacheService _cacheService)
    : IStateMachine
{
    public async ValueTask HandlerAsync(string json, CancellationToken token = default)
    {
        var barCode = _controller.GetBarCodeByDeviceName(json);
        if (string.IsNullOrEmpty(barCode))
        {
            Log.Logger.BusinessInformation(LogCategory.Business,
                new BusinessLog(json, -9999, "扫码信息为空无法获取执行任务"));
            return;
        }

        var wcsTasks = await _sender.Send(new GetExecuteTaskQuery
        {
            DeviceName = json,
            DeviceType = DeviceTypeEnum.StockPortIn,
            Region = _controller.GetRegionCodesByDeviceName(json),
            TaskCacheKey = _controller.GetWcsTaskCacheOfKey(json),
            WcsTaskType = WcsTaskTypeEnum.StockIn,
            TaskStatus = WcsTaskStatusEnum.Analysited,
            Container = barCode
        });
        if (wcsTasks.Any())
        {
            var wcsTask = wcsTasks.First();
            if (wcsTask != null)
            {
                wcsTask.TaskExecuteStep.CurentDevice = json;
                wcsTask.TaskExecuteStep.DeviceType = DeviceTypeEnum.StockPortIn;
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.ApplyTunnle;
                _controller.SetWcsTask(json, wcsTask);
                await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(json), wcsTask);
                Log.Logger.BusinessInformation(LogCategory.Business,
                    new BusinessLog(json, wcsTask.SerialNumber,
                        $"扫码信息为:{barCode};获取到任务号为:{wcsTask.SerialNumber}的执行任务"));
            }
            else
            {
                Log.Logger.BusinessInformation(LogCategory.Business,
                    new BusinessLog(json, -9999,
                        $"扫码信息为:{_controller.GetBarCodeByDeviceName(json)};未获取到数据"));
            }
        }
    }
}