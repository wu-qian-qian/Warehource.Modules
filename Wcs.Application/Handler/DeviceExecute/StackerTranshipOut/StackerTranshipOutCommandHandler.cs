using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Handler.Business.CheckExecuteNode;
using Wcs.Application.Handler.Business.FilterStackerTask;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.CustomEvents.Saga;
using Wcs.Device.Device.Tranship;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipOut;

internal class StackerTranshipOutCommandHandler(
    ISender _sender,
    ICacheService _cacheService,
    IPublishEndpoint _publishEndpoint)
    : ICommandHandler<StackerTranshipOutCommand>
{
    public async Task Handle(StackerTranshipOutCommand request, CancellationToken cancellationToken)
    {
        var device = request.Device;
        if (device.CanExecute())
        {
            var wcsTask = await _cacheService.GetAsync<WcsTask>(device.Config.TaskKey);
            if (wcsTask == null)
            {
                var wcsTasks = await _sender.Send(new GetFilterStackerQuery
                {
                    DeviceType = device.DeviceType,
                    DeviceName = device.Name
                });
                if (wcsTasks.Any())
                {
                    wcsTask = wcsTasks.First();
                    await _cacheService.SetAsync(device.Config.TaskKey, wcsTask);
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{device.Name}:获取执行任务");
                }
            }
            else
            {
                if (device.IsNewStart())
                {
                    if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.ToBeSend)
                    {
                        var check = await _sender.Send(new CheckExecuteNodeCommand
                        {
                            WcsTask = wcsTask,
                            DeviceRegionCode = device.RegionCodes,
                            Title = wcsTask.StockOutPosition
                        }, cancellationToken);
                        wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                        if (check.IsSuccess)
                            if (check.Value)
                            {
                                await _cacheService.SetAsync(device.Config.TaskKey, wcsTask);
                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{device.Name}:发送执行任务");
                            }
                    }
                    else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.SendEnding)
                    {
                        await WriteTaskData(device, wcsTask);
                        wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
                        await _cacheService.SetAsync(device.Config.TaskKey, wcsTask);
                    }
                    else if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.Complate)
                    {
                        await _sender.Send(new RefreshTaskStatusCommand
                            { Key = device.Config.TaskKey, WcsTask = wcsTask });
                    }
                }
            }
        }
    }

    private async Task WriteTaskData(AbstractStackerTranship device, WcsTask wcsTask)
    {
        var dic = new Dictionary<string, string>();
        dic.Add(device.CreatWriteExpression(p => p.WTask), wcsTask.SerialNumber.ToString());
        dic.Add(device.CreatWriteExpression(p => p.WTargetCode), wcsTask.StockOutPosition);
        dic.Add(device.CreatWriteExpression(p => p.WTaskType), ((int)wcsTask.TaskType).ToString());
        dic.Add(device.CreatWriteExpression(p => p.WStart), "1");
        await _publishEndpoint.Publish(new WcsWritePlcTaskCreated(device.Name, dic, device.Config.TaskKey));
        wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.BeSending;
    }
}