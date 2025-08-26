using Common.Application.MediatR.Message;
using MediatR;
using Wcs.Application.Handler.Business.CheckExecuteNode;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DeviceExecute.StockOut;

internal class StockOutCommandHandler(IWcsTaskRepository _wcsTaskRepository, ISender _sender)
    : ICommandHandler<StockOutCommand>
{
    public async Task Handle(StockOutCommand request, CancellationToken cancellationToken)
    {
        var device = request.Device;
        if (device.CanExecute())
        {
            var wcsTask = _wcsTaskRepository.Get(device.DBEntity.RTask);
            if (wcsTask.TaskExecuteStep.CurentDevice == device.Name
                && wcsTask.TaskExecuteStep.DeviceType == device.DeviceType)
            {
                var check = await _sender.Send(new CheckExecuteNodeCommand
                {
                    DeviceRegionCode = device.RegionCodes,
                    WcsTask = wcsTask
                });
                if (check.IsSuccess)
                    if (check.Value)
                    {
                        //TODO 如果路径未结束
                    }
            }
        }
    }
}