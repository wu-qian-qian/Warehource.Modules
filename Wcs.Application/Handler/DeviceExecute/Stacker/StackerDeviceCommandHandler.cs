using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.CheckExecuteNode;
using Wcs.Application.Handler.Business.FilterStackerTask;
using Wcs.Application.Handler.Business.GetStackerStation;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.CustomEvents.Saga;
using Wcs.Device.DeviceStructure.Stacker;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute.Stacker;

/// <summary>
///     堆垛机业务处理事件
/// </summary>
/// <param name="_sender"></param>
internal class StackerDeviceCommandHandler(
    ISender _sender,
    IPublishEndpoint _publishEndpoint,
    ICacheService _cacheService,
    IDeviceService _deviceService,
    IAnalysisLocation _locationService)
    : ICommandHandler<StackerDeviceCommand>
{
    public async Task Handle(StackerDeviceCommand request, CancellationToken cancellationToken)
    {
        var stacker = request.Device;
        //判断是否可执行
        if (stacker.CanExecute())
        {
            //获取当前堆垛机执行的任务
            var wcsTask = await _cacheService.GetAsync<WcsTask>(stacker.Config.TaskKey, cancellationToken);
            //当前堆垛机无执行任务则从数据库中查找执行
            if (wcsTask == null)
            {
                //获取该设备所有任务
                var wcsTasks = await _sender.Send(new GetFilterWcsTaskQuery
                        { DeviceName = stacker.Name, DeviceType = stacker.DeviceType }
                    , cancellationToken);
                //获取到执行任务
                wcsTask = await _sender.Send(new FilterStackerCommand
                {
                    IsTranShipPoint = stacker.IsTranShipPoint(),
                    WcsTasks = wcsTasks
                });
                if (wcsTask != null)
                {
                    await _cacheService.SetAsync(stacker.Config.TaskKey, wcsTask,
                        cancellationToken: cancellationToken);
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{stacker.Name}-获取执行任务-{wcsTask.SerialNumber}");
                }
            }
            else
            {
                if (stacker.IsComplate())
                {
                    var check = await _sender.Send(new CheckExecuteNodeCommand
                    {
                        WcsTask = wcsTask,
                        DeviceRegionCode = stacker.RegionCodes,
                        Title = stacker.Config.Tunnle
                    }, cancellationToken);
                    if (check.IsSuccess)
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information($"{stacker.Name}-堆垛机执行任务完成-{wcsTask.SerialNumber}");
                        await _sender.Send(new RefreshTaskStatusCommand
                            { Key = stacker.Config.TaskKey, WcsTask = wcsTask });
                    }
                    else
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information($"{stacker.Name}-节点检测失败-{wcsTask.SerialNumber}--{check.Message}");
                    }
                }
                else
                {
                    //写入数据
                    if (stacker.IsNewStart())
                        if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.ToBeSend)
                        {
                            var result = await _sender.Send(new GetStackerStaionCommand
                            {
                                WcsTask = wcsTask,
                                Region = stacker.RegionCodes
                            });
                            if (result.IsSuccess)
                            {
                                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendEnding;
                                await _cacheService.SetAsync(stacker.Config.TaskKey, wcsTask,
                                    cancellationToken: cancellationToken);
                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{wcsTask.SerialNumber}任务数据更新");
                            }
                            else
                            {
                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{wcsTask.SerialNumber}无法更新排列数据");
                            }
                        }
                        else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.SendEnding)
                        {
                            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                            await _cacheService.SetAsync(stacker.Config.TaskKey, wcsTask,
                                cancellationToken: cancellationToken);
                            await WriteTaskDataAsync(stacker, wcsTask);
                            Log.Logger.ForCategory(LogCategory.Business)
                                .Information("任务数据写入");
                        }
                }
            }
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{stacker.Name}--堆垛机连接异常无法执行任务");
        }
    }

    private async Task WriteTaskDataAsync(AbstractStacker stacker, WcsTask wcsTask)
    {
        var dic = new Dictionary<string, string>();
        dic.Add(stacker.CreatWriteExpression(p => p.WTask), wcsTask.SerialNumber.ToString());
        dic.Add(stacker.CreatWriteExpression(p => p.WGetColumn), wcsTask.GetLocation.GetColumn);
        dic.Add(stacker.CreatWriteExpression(p => p.WGetFloor), wcsTask.GetLocation.GetFloor);
        dic.Add(stacker.CreatWriteExpression(p => p.WGetRow), wcsTask.GetLocation.GetRow);
        dic.Add(stacker.CreatWriteExpression(p => p.WPutColumn), wcsTask.PutLocation.PutColumn);
        dic.Add(stacker.CreatWriteExpression(p => p.WPutFloor), wcsTask.PutLocation.PutFloor);
        dic.Add(stacker.CreatWriteExpression(p => p.WPutRow), wcsTask.PutLocation.PutRow);
        dic.Add(stacker.CreatWriteExpression(p => p.WTaskType), ((int)wcsTask.TaskType).ToString());
        dic.Add(stacker.CreatWriteExpression(p => p.WStart), "1");
        await _publishEndpoint.Publish(new WcsWritePlcTaskCreated(stacker.Name, dic,
            stacker.Config.TaskKey));
    }
}