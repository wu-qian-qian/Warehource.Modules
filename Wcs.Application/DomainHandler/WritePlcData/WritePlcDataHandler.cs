using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Application.StateMachine;
using Common.Domain.State;
using Common.Shared;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Application.DomainEvent.WritePlcData
{
    internal class WritePlcDataHandler(ICacheService _cacheService, IStateMachineManager _FSM)
        : IEventHandler<WritePlcDataEvent>
    {
        /// <summary>
        /// 处理PLC写入结果
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask Handle(WritePlcDataEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var wcsTask =
                await _cacheService.GetAsync<Wcs.Domain.Task.WcsTask>(domainEvent.CacheKey, cancellationToken);
            //
            if (wcsTask != null)
            {
                if (domainEvent.IsSuccess == true)
                {
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = Shared.TaskExecuteStepTypeEnum.SendEndingPlc;
                }
                else
                {
                    //应该添加一个状态机为写入失败 这里偷懒了
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = Shared.TaskExecuteStepTypeEnum.SendPlc;
                }

                await _cacheService.SetAsync(domainEvent.CacheKey, wcsTask);
                var key = $"{wcsTask.TaskExecuteStep.DeviceType}-{wcsTask.TaskExecuteStep.TaskExecuteStepType}";
                await _FSM.NextStatus(key, wcsTask.TaskExecuteStep.CurentDevice);
            }
            else
            {
                Serilog.Log.Logger.ForCategory(LogCategory.Event)
                    .Information("WritePlcDataHandler未找到对应的WcsTask，CacheKey:{CacheKey}", domainEvent.CacheKey);
            }
        }
    }
}