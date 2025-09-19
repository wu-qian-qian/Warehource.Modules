using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.CheckExecuteNode;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.Application.Handler.Http.ApplyTunnle;
using Wcs.CustomEvents.Saga;
using Wcs.Device.Device.StockPort;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute.StockIn;

/// <summary>
/// </summary>
/// <param name="_taskRepository"></param>
/// <param name="_sender"></param>
/// <param name="_cacheService"></param>
/// <param name="_publishEndpoint"></param>
/// <param name="_deviceService"></param>
internal class StockInCommandHandler(
    IWcsTaskRepository _taskRepository,
    ISender _sender,
    ICacheService _cacheService,
    IPublishEndpoint _publishEndpoint,
    IDeviceService _deviceService)
    : ICommandHandler<StockInCommand>
{
    public async Task Handle(StockInCommand request, CancellationToken cancellationToken)
    {
        var stockIn = request.Device;
        var wcsTask = await _cacheService.GetAsync<WcsTask>(stockIn.Config.TaskKey);
        //表示
        if (stockIn.CanExecute())
        {
            //存在任务，且输送可以执行
            if (wcsTask != null && stockIn.IsNewStart())
            {
                //可考虑使用状态机
                if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.ToBeSend)
                {
                    var @bool = await _sender.Send(new ApplyTunnleCommand
                    {
                        WcsTask = wcsTask
                    }, cancellationToken);
                    if (@bool)
                    {
                        var check = await _sender.Send(new CheckExecuteNodeCommand
                        {
                            WcsTask = wcsTask,
                            DeviceRegionCode = stockIn.RegionCodes,
                            Title = wcsTask.GetLocation.GetTunnel
                        });
                        if (check.IsSuccess)
                        {
                            if (check.Value)
                            {
                                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendEnding;
                                //任务数据更新缓存
                                await _cacheService.SetAsync(stockIn.Config.TaskKey, wcsTask);
                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{wcsTask.SerialNumber}--发送执行任务");
                            }
                        }
                        else
                        {
                            Log.Logger.ForCategory(LogCategory.Business)
                                .Information($"{stockIn.Name}：{check.Message}");
                        }
                    }
                    else
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information($"{wcsTask.SerialNumber}--未申请到巷道");
                    }
                }
                else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.SendEnding)
                {
                    //重新发送
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                    await _cacheService.SetAsync(stockIn.Config.TaskKey, wcsTask);
                    var targetCode =
                        await _deviceService.GetTargetPipelinAsync(wcsTask.TaskExecuteStep.DeviceType.Value
                            , wcsTask.TaskExecuteStep.CurentDevice);
                    //数据写入
                    await WriteTaskData(stockIn, wcsTask, targetCode);

                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{wcsTask.SerialNumber}--重发执行任务");
                }
                else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.Complate)
                {
                    //任务数据更新到数据库
                    await _sender.Send(new RefreshTaskStatusCommand
                        { Key = stockIn.Config.TaskKey, WcsTask = wcsTask });
                }
            }
            else
            {
                wcsTask = _taskRepository.GetWcsTaskQuerys()
                    .Where(p => p.TaskExecuteStep.DeviceType == stockIn.DeviceType)
                    .Where(p => p.TaskStatus == WcsTaskStatusEnum.Analysited)
                    .FirstOrDefault(p => p.Container == stockIn.DBEntity.RBarCode);
                if (wcsTask != null)
                {
                    await _cacheService.SetAsync(stockIn.Config.TaskKey, wcsTask);
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{stockIn.Name}:获取到执行任务{wcsTask.Id}-{wcsTask.Container}");
                }
                else
                {
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{stockIn.Name}:未获取到执行任务");
                }
            }
        }
    }

    private async Task WriteTaskData(AbstractStockPort stockIn, WcsTask wcsTask, string targetCode)
    {
        var dic = new Dictionary<string, string>();
        dic.Add(stockIn.CreatWriteExpression(p => p.WTask), wcsTask.SerialNumber.ToString());
        dic.Add(stockIn.CreatWriteExpression(p => p.WTargetCode), targetCode);
        dic.Add(stockIn.CreatWriteExpression(p => p.WTaskType), ((int)wcsTask.TaskType).ToString());
        dic.Add(stockIn.CreatWriteExpression(p => p.WStart), "1");
        await _publishEndpoint.Publish(new WcsWritePlcTaskCreated(stockIn.Name, dic,
            stockIn.Config.TaskKey));
        wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
    }
}