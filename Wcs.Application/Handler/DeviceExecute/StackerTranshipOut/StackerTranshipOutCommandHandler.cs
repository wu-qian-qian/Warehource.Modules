using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Handler.Business.RefreshTaskStatus;
using Wcs.CustomEvents.Saga;
using Wcs.Device.DeviceStructure.Tranship;
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
    }

    private async Task WriteTaskData(AbstractStackerTranship device, WcsTask wcsTask)
    {
        var dic = new Dictionary<string, string>();
        dic.Add(device.CreatWriteExpression(p => p.WTask), wcsTask.SerialNumber.ToString());
        dic.Add(device.CreatWriteExpression(p => p.WTargetCode), wcsTask.EndPosition);
        dic.Add(device.CreatWriteExpression(p => p.WTaskType), ((int)wcsTask.TaskType).ToString());
        dic.Add(device.CreatWriteExpression(p => p.WStart), "1");
        await _publishEndpoint.Publish(new WcsWritePlcTaskCreated(device.Name, dic, device.Config.TaskKey));
        wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.None;
    }
}