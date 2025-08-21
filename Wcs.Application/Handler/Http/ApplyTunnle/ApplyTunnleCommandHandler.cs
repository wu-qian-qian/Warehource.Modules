using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Business;
using Wcs.Shared;

namespace Wcs.Application.Handler.Http.ApplyTunnle;

public class ApplyTunnleCommandHandler(IDeviceService _deviceService)
    : ICommandHandler<ApplyTunnleCommand, Result<RecommendTunnle>>
{
    public async Task<Result<RecommendTunnle>> Handle(ApplyTunnleCommand request, CancellationToken cancellationToken)
    {
        Result<RecommendTunnle> result = new();
        // TODO 通过调用上游系统获取巷道  ，或者根据库容率获取最合适的巷道
        var recommendTunnle = await _deviceService
            .GetRecommendTunnleAsync(DeviceTypeEnum.StackerInTranShip, request.Tunnles.First());

        if (recommendTunnle != null)
            result.SetValue(recommendTunnle);
        else
            result.SetMessage("未获取到，推荐巷道设备");

        return result;
    }
}