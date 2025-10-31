using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Application.StateMachine;
using Common.Shared;
using Serilog;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.DomainEvent.WritePlcData;

internal class WritePlcDataHandler(ICacheService _cacheService, IStateMachineManager _FSM)
    : IEventHandler<WritePlcDataEvent>
{
    /// <summary>
    ///     处理PLC写入结果
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(WritePlcDataEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var wcsTask =
            await _cacheService.GetAsync<WcsTask>(domainEvent.CacheKey, cancellationToken);
        //
        if (wcsTask != null)
        {
            if (domainEvent.IsSuccess)
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendEndingPlc;
            else
                //应该添加一个状态机为写入失败 这里偷懒了
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendPlc;

            await _cacheService.SetAsync(domainEvent.CacheKey, wcsTask);
            var key = $"{wcsTask.TaskExecuteStep.DeviceType}-{wcsTask.TaskExecuteStep.TaskExecuteStepType}";
            await _FSM.NextStatus(key, wcsTask.TaskExecuteStep.CurentDevice);
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Event)
                .Information("WritePlcDataHandler未找到对应的WcsTask，CacheKey:{CacheKey}", domainEvent.CacheKey);
        }
    }
}