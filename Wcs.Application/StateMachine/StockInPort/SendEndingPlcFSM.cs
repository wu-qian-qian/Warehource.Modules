using Common.Application.Caching;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.StokInPort
{
    /// <summary>
    /// 入库口发送完成PLC状态机
    /// </summary>
    /// <param name="_cacheService"></param>
    /// <param name="_controller"></param>
    [StateAttrubite($"{nameof(DeviceTypeEnum.StockPortIn)}-{nameof(TaskExecuteStepTypeEnum.SendEndingPlc)}")]
    internal class SendEndingPlcFSM(ICacheService _cacheService, IStockPortInController _controller) : IStateMachine
    {
        public async ValueTask HandlerAsync(string json, CancellationToken token = default)
        {
            var cacheKey = _controller.GetWcsTaskCacheOfKey(json);
            var wcsTask = _controller.GetWcsTaskByDeviceName(json);
            var cacheTask = await _cacheService.GetAsync<WcsTask>(cacheKey, token);
            if (cacheTask != null &&
                cacheTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.SendEndingPlc)
            {
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.Complate;
                Log.Logger
                    .BusinessInformation(LogCategory.Business, new Common.Shared.Log.BusinessLog(json,
                        wcsTask.SerialNumber,
                        "数据写入成功，状态机更新下一状态"));
                await _cacheService.SetAsync<WcsTask>(cacheKey, wcsTask);
            }
            else
            {
                //这里重写 如果上游状态机触发过来没有任务说明更新缓存失败
                Log.Logger
                    .BusinessInformation(LogCategory.Business, new Common.Shared.Log.BusinessLog(json,
                        wcsTask.SerialNumber,
                        "输入写入失败，状态机返回发送PLC输入状态"));
            }
        }
    }
}