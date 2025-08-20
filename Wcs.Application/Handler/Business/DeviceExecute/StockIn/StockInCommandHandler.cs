using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.RefreshExecuteType;
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
                    var executeNodePath = _nodeRepository.GetQuerys()
                        .Where(p => p.PahtNodeGroup == wcsTask.TaskExecuteStep.PathNodeGroup).ToArray();
                    if (executeNodePath.All(p => stockIn.RegionCodes.Contains(p.Region.Code)))
                    {
                        var index = executeNodePath.First(p => p.CurrentDeviceType == stockIn.DeviceType).Index;
                        var nextType = index++;
                        var executeNode = executeNodePath.FirstOrDefault(p => p.Index == nextType);
                        if (executeNode != null)
                        {
                            var targetCode =
                                await _deviceService.GerRecommendTunnleAsync(executeNode.CurrentDeviceType);
                            //刷新任务数据
                            wcsTask.TaskExecuteStep.CurentDevice = targetCode.DeviceName;
                            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                            wcsTask.TaskExecuteStep.DeviceType = executeNode.CurrentDeviceType;
                            wcsTask.TaskStatus = WcsTaskStatusEnum.InProgress;

                            await _cacheService.SetAsync(stockIn.Config.Key, wcsTask);
                            Log.Logger.ForCategory(LogCategory.Business)
                                .Information($"{wcsTask.SerialNumber}--发送执行任务");
                            var dic = new Dictionary<string, string>();
                            await _publishEndpoint.Publish(new WcsWritePlcTaskCreated(stockIn.Name, dic,
                                stockIn.Config.TaskKey));
                        }
                    }
                    else
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information($"设备，不能执行该区域的任务{executeNodePath[0].Region.Code}");
                    }
                }
                else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.SendEnding)
                {
                    //重新发送
                    wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                    await _cacheService.SetAsync(stockIn.Config.Key, wcsTask);
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{wcsTask.SerialNumber}--重发执行任务");
                    //数据写入
                    var dic = new Dictionary<string, string>();
                    await _publishEndpoint.Publish(
                        new WcsWritePlcTaskCreated(stockIn.Name, dic, stockIn.Config.TaskKey));
                }
                else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.Complate)
                {
                    //任务数据更新到数据库
                    await _sender.Send(new RefreshExecuteTypeCommand { Key = stockIn.Config.TaskKey });
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