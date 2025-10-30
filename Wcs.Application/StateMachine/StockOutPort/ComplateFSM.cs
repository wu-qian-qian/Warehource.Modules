using Common.Application.Caching;
using Common.Domain.State;
using Common.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.Handler.Business.GetNextExecuteNode;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.StokOutPort
{
    /// <summary>
    /// 入库口完成状态机
    /// </summary>
    /// <param name="_sender"></param>
    /// <param name="_cacheService"></param>
    /// <param name="_controller"></param>
    [StateAttrubite($"{nameof(DeviceTypeEnum.StockPortOut)}-{nameof(TaskExecuteStepTypeEnum.Complate)}")]
    internal class ComplateFSM(ISender _sender, ICacheService _cacheService, IStockPortInController _controller)
        : IStateMachine
    {
        public async ValueTask HandlerAsync(string json, CancellationToken token = default)
        {
            var wcsTask = _controller.GetWcsTaskByDeviceName(json);
            if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.Complate)
            {
                //还差一步为更新下一节点
                var result = await _sender.Send(new GetNextExecuteNodeQuery
                {
                    PathNodeGroup = wcsTask.TaskExecuteStep.PathNodeGroup,
                    DeviceType = wcsTask.TaskExecuteStep.DeviceType.Value,
                    RegionCode = wcsTask.RegionCode,
                    WcsTaskType = wcsTask.TaskType
                });
                if (result.IsSuccess)
                {
                    if (result.Value == wcsTask.TaskExecuteStep.DeviceType)
                    {
                        wcsTask.TaskExecuteStep.DeviceType = result.Value;
                        wcsTask.TaskExecuteStep.CurentDevice = json;
                        wcsTask.TaskStatus = WcsTaskStatusEnum.Completed;
                    }
                    else
                    {
                        wcsTask.TaskExecuteStep.DeviceType = result.Value;
                        wcsTask.TaskExecuteStep.CurentDevice = string.Empty;
                        wcsTask.TaskStatus = WcsTaskStatusEnum.InProgress;
                    }

                    await _sender.Send(new RefreshTaskStatusCommand
                        { WcsTask = wcsTask });
                    var key = _controller.GetWcsTaskCacheOfKey(json);
                    await _cacheService.RemoveAsync(key);
                    _controller.CleatWcsTaskByDeviceName(json);
                }
            }
        }
    }
}