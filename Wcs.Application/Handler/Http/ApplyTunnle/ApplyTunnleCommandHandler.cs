using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Business;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Http.ApplyTunnle;

public class ApplyTunnleCommandHandler(IDeviceService _deviceService)
    : ICommandHandler<ApplyTunnleCommand, bool>
{
    public async Task<bool> Handle(ApplyTunnleCommand request, CancellationToken cancellationToken)
    {
        var @bool = false;
        var tunnles =
            await _deviceService.GetCanExecuteTunnleAsync(request.WcsTask.TaskExecuteStep.DeviceType.Value);
        Result<ReCommendTunnle> result = new();
        // TODO 通过调用上游系统获取巷道  ，或者根据库容率获取最合适的巷道
        var tunnle = tunnles[0];
        var location = await _deviceService
            .GetTranshipPositionAsync(request.WcsTask.TaskExecuteStep.DeviceType.Value, tunnle);
        var pos = location.Split("_");
        if (pos.Length > 3)
        {
            var getLocation = new GetLocation(pos[0],
                pos[1], pos[2]
                , pos[3], string.Empty);
            request.WcsTask.GetLocation = getLocation;
            @bool = true;
        }

        return @bool;
    }
}