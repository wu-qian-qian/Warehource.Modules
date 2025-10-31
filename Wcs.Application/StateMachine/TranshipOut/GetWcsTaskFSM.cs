using Common.Application.Caching;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using MediatR;
using Serilog;
using Wcs.Application.DeviceController.Tranship.TranshipOut;
using Wcs.Application.Handler.Business.GetExecuteTask;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.TranshipOut;

/// <summary>
///     获取WCS任务状态机
/// </summary>
/// <param name="_controller"></param>
/// <param name="_sender"></param>
/// <param name="_cacheService"></param>
[StateAttrubite($"{nameof(DeviceTypeEnum.StackerOutTranShip)}-{nameof(TaskExecuteStepTypeEnum.GetWcsTask)}")]
internal class GetWcsTaskFSM(
    IStackerTranshipOutController _controller,
    ISender _sender,
    ICacheService _cacheService) : IStateMachine
{
    public async ValueTask HandlerAsync(string json, CancellationToken token = default)
    {
        //涉及到设备的任务数据的转换传输 所以任务数据应该从缓存中获取
        var wcsTasks = await _sender.Send(new GetExecuteTaskQuery
        {
            TaskCacheKey = _controller.GetWcsTaskCacheOfKey(json)
        });
        if (wcsTasks.Any())
        {
            var wcsTask = wcsTasks.First();
            wcsTask.TaskExecuteStep.CurentDevice = json;
            wcsTask.StartPosition = _controller.GetPiplineByDeviceName(json);
            wcsTask.TaskExecuteStep.DeviceType = DeviceTypeEnum.StackerOutTranShip;
            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendPlc;
            _controller.SetWcsTask(json, wcsTask);
            await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(json), wcsTask);
            Log.Logger.BusinessInformation(LogCategory.Business,
                new BusinessLog(json, wcsTask.SerialNumber, "接驳位获取到执行任务数据"));
        }
    }
}