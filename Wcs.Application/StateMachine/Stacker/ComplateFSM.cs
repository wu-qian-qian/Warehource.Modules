using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.DeviceController.Stacker;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.DomainHandler.InformTranShipOut;
using Wcs.Application.Handler.Business.GetNextExecuteNode;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.Stacker
{
    /// <summary>
    /// 入库口完成状态机
    /// </summary>
    /// <param name="_sender"></param>
    /// <param name="_cacheService"></param>
    /// <param name="_controller"></param>
    [StateAttrubite($"{nameof(DeviceTypeEnum.Stacker)}-{nameof(TaskExecuteStepTypeEnum.Complate)}")]
    internal class ComplateFSM(
        ISender _sender,
        ICacheService _cacheService,
        IStackerController _controller,
        IEventBus _bus) : IStateMachine
    {
        public async ValueTask HandlerAsync(string json, CancellationToken token = default)
        {
            var wcsTask = _controller.GetWcsTaskByDeviceName(json);
            bool isComplate = _controller.IsComplateByDeviceName(json);
            if (isComplate == true)
            {
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
                            //发布事件通知下一个设备   下一个设备不知道我需要执行的任务所以这边通过事件总线通知下一个设备需要执行的任务
                            await _bus.PublishAsync(new InformTranShipOutEvent { WcsTask = wcsTask }, token);
                            Serilog.Log.Logger
                                .BusinessInformation(LogCategory.Business,
                                    new Common.Shared.Log.BusinessLog(json, wcsTask.SerialNumber,
                                        $"将下一阶段设备的执行的任务数据插入缓存"));
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
}