using Common.Application.Caching;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Common.Shared.Log;
using MassTransit;
using Serilog;
using Wcs.Application.DeviceController.Stacker;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.Stacker;

/// <summary>
///     发送PLC状态机
/// </summary>
/// <param name="_controller"></param>
/// <param name="_publishEndpoint"></param>
/// <param name=""></param>
/// <param name="_cacheService"></param>
[StateAttrubite($"{nameof(DeviceTypeEnum.Stacker)}-{nameof(TaskExecuteStepTypeEnum.SendPlc)}")]
internal sealed class SendPlcFMS(
    IStackerController _controller,
    IPublishEndpoint _publishEndpoint,
    ICacheService _cacheService) : IStateMachine
{
    public async ValueTask HandlerAsync(string json, CancellationToken token = default)
    {
        var wcsTask = _controller.GetWcsTaskByDeviceName(json);
        //这里涉及到数据发送逻辑所以必须进行判断
        if (wcsTask.TaskExecuteStep.TaskExecuteStepType != TaskExecuteStepTypeEnum.SendPlc)
        {
            //重新发送
            Log.Logger
                .BusinessInformation(LogCategory.Business, new BusinessLog(json,
                    wcsTask.SerialNumber,
                    "状态机处理状态错误，无法进行业务逻辑处理"));
        }
        else
        {
            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSendingPlc;
            var sendData = _controller.TryGetWritePlcTaskData(json);
            await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(json), wcsTask);
            await _publishEndpoint.Publish(sendData);
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{wcsTask.SerialNumber}--重发执行任务");
        }
    }
}