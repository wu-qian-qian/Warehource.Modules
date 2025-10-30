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
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.StateMachine.StokOutPort
{
    /// <summary>
    /// 入库口获取WCS任务状态机
    /// </summary>
    /// <param name="_controller"></param>
    /// <param name="_sender"></param>
    /// <param name="_cacheService"></param>
    [StateAttrubite($"{nameof(DeviceTypeEnum.StockPortOut)}-{nameof(TaskExecuteStepTypeEnum.GetWcsTask)}")]
    internal class GetWcsTaskFSM(IStockPortOutController _controller, ISender _sender, ICacheService _cacheService)
        : IStateMachine
    {
        public async ValueTask HandlerAsync(string json, CancellationToken token = default)
        {
            var wcsTaskCode = _controller.GetWcsTaskCodeByDeviceName(json);
            var wcsTask = default(WcsTask);
            if (!string.IsNullOrEmpty(wcsTaskCode))
            {
                var wcsTasks = await _sender.Send(new GetExecuteTaskQuery { SerialNumber = int.Parse(wcsTaskCode) },
                    token);
                if (wcsTasks.Any())
                {
                    wcsTask = wcsTasks.First();
                    wcsTask.TaskExecuteStep.CurentDevice = json;
                    wcsTask.TaskExecuteStep.DeviceType = DeviceTypeEnum.StockPortOut;
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.Complate;
                }
            }

            if (wcsTask != null)
            {
                Serilog.Log.Logger
                    .BusinessInformation(LogCategory.Business,
                        new Common.Shared.Log.BusinessLog(json, -9999, $"获取到执行任务数据"));
            }
        }
    }
}