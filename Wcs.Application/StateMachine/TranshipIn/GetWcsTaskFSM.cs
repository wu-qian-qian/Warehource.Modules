using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.State;
using Common.Shared;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.DomainEvent.ApplyTunnle;
using Wcs.Application.Handler.Business.GetExecuteTask;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.TranshipIn
{
    /// <summary>
    /// 入库口获取WCS任务状态机
    /// </summary>
    /// <param name="_controller"></param>
    /// <param name="_sender"></param>
    /// <param name="_cacheService"></param>
    [StateAttrubite($"{nameof(DeviceTypeEnum.StackerInTranShip)}-{nameof(TaskExecuteStepTypeEnum.GetWcsTask)}")]
    internal class GetWcsTaskFSM(IStackerTranshipInController _controller, ISender _sender, ICacheService _cacheService)
        : IStateMachine
    {
        public async ValueTask HandlerAsync(string json, CancellationToken token = default)
        {
            var wcsTasks = await _sender.Send(new GetExecuteTaskQuery
            {
                DeviceName = json,
                DeviceType = DeviceTypeEnum.StackerInTranShip,
                Region = _controller.GetRegionCodesByDeviceName(json),
                TaskCacheKey = _controller.GetWcsTaskCacheOfKey(json),
                WcsTaskType = WcsTaskTypeEnum.StockIn,
                TaskStatus = WcsTaskStatusEnum.InProgress,
                GetTunnle = _controller.GetTunnleByDeviceName(json),
            });
            if (wcsTasks.Any())
            {
                var wcsTask = wcsTasks.FirstOrDefault(p => p.Container == _controller.GetWcsTaskNoByDeviceName(json));
                if (wcsTask != null)
                {
                    wcsTask.TaskExecuteStep.CurentDevice = json;
                    wcsTask.TaskExecuteStep.DeviceType = DeviceTypeEnum.StackerInTranShip;
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.ApplyLocation;
                    _controller.SetWcsTask(json, wcsTask);
                    await _cacheService.SetAsync(_controller.GetWcsTaskCacheOfKey(json), wcsTask);
                }
                else
                {
                    Serilog.Log.Logger.BusinessInformation(LogCategory.Business,
                        new Common.Shared.Log.BusinessLog(json, -9999,
                            $"输送任务数据为:{_controller.GetWcsTaskNoByDeviceName(json)};未获取到数据"));
                }
            }
        }
    }
}