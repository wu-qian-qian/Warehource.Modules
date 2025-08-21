using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.CheckExecuteNode;
using Wcs.Application.Handler.Business.RefreshExecuteType;
using Wcs.Application.Handler.Http.ApplyTunnle;
using Wcs.CustomEvents.Saga;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.DeviceExecute.StockIn;

internal class StockInCommandHandler(
    IWcsTaskRepository _taskRepository,
    ISender _sender,
    ICacheService _cacheService,
    IPublishEndpoint _publishEndpoint,
    IExecuteNodeRepository _nodeRepository,
    IDeviceService _deviceService)
    : ICommandHandler<StockInCommand>
{
    public async Task Handle(StockInCommand request, CancellationToken cancellationToken)
    {
        var stockIn = request.StockIn;
        var wcsTask = await _cacheService.GetAsync<WcsTask>(stockIn.Config.TaskKey);
        //表示
        if (wcsTask != null)
        {
            if (stockIn.CanExecute())
            {
                //可考虑使用状态机
                if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.ToBeSend)
                {
                    var check = await _sender.Send(new CheckExecuteNodeCommand
                    {
                        WcsTask = wcsTask,
                        DeviceRegionCode = stockIn.RegionCodes
                    });
                    if (check.IsSuccess)
                    {
                        //获取推荐巷道集合
                        var recommendTunnle =
                            await _deviceService.GetCanExecuteTunnleAsync(wcsTask.TaskExecuteStep.DeviceType.Value);
                        //刷新任务数据
                        if (recommendTunnle != null)
                        {
                            //获取巷道准确的巷道
                            var result = await _sender.Send(new ApplyTunnleCommand
                            {
                                WcsTaskCode = wcsTask.TaskCode,
                                Tunnles = recommendTunnle
                            }, cancellationToken);
                            if (result.IsSuccess)
                            {
                                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                                wcsTask.TaskStatus = WcsTaskStatusEnum.InProgress;
                                //任务数据更新缓存
                                await _cacheService.SetAsync(stockIn.Config.Key, wcsTask);
                                //发送写入任务数据事件
                                var dic = new Dictionary<string, string>();
                                await _publishEndpoint.Publish(new WcsWritePlcTaskCreated(stockIn.Name, dic,
                                    stockIn.Config.TaskKey));

                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{wcsTask.SerialNumber}--发送执行任务");
                            }
                            else
                            {
                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{wcsTask.SerialNumber}--无法获取到执行设备");
                            }
                        }
                        else
                        {
                            Log.Logger.ForCategory(LogCategory.Business)
                                .Information($"{wcsTask.SerialNumber}--无法获取推荐巷道");
                        }
                    }
                    else
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information($"{stockIn.Name}：{check.Message}");
                    }
                }
                else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.SendEnding)
                {
                    //重新发送
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                    await _cacheService.SetAsync(stockIn.Config.Key, wcsTask);
                    var targetCode =
                        await _deviceService.GetTargetPipelinAsync(wcsTask.TaskExecuteStep.DeviceType.Value
                            , wcsTask.TaskExecuteStep.CurentDevice);
                    //数据写入
                    var dic = new Dictionary<string, string>();
                    await _publishEndpoint.Publish(
                        new WcsWritePlcTaskCreated(stockIn.Name, dic, stockIn.Config.TaskKey));
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{wcsTask.SerialNumber}--重发执行任务");
                }
                else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.Complate)
                {
                    //任务数据更新到数据库
                    await _sender.Send(new RefreshTaskStatusCommand { Key = stockIn.Config.TaskKey });
                }
            }
        }
        else
        {
            if (stockIn.IsNewStart())
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
}