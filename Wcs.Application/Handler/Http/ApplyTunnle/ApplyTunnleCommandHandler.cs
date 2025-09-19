using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Domain.Region;
using Wcs.Shared;

namespace Wcs.Application.Handler.Http.ApplyTunnle;

public class ApplyTunnleCommandHandler(
    IDeviceService _deviceService,
    IAnalysisLocation _locationService,
    IRegionRepository _regionRepository)
    : ICommandHandler<ApplyTunnleCommand, bool>
{
    public async Task<bool> Handle(ApplyTunnleCommand request, CancellationToken cancellationToken)
    {
        var region = _regionRepository.Get(request.WcsTask.RegionId.Value);
        var @bool = false;
        var tunnle = string.Empty;
        if (request.WcsTask.CreatorSystemType != CreatorSystemTypeEnum.Other)
        {
            var tunnles =
                await _deviceService.GetCanExecuteTunnleAsync(request.WcsTask.TaskExecuteStep.DeviceType.Value,
                    region.Code);
            // TODO 通过调用上游系统获取巷道  ，或者根据库容率获取最合适的巷道
        }
        else
        {
            tunnle = request.WcsTask.PutLocation.PutTunnel;
        }

        if (tunnle != string.Empty && tunnle != null)
        {
            var location = await _deviceService
                .GetTranshipPositionAsync(request.WcsTask.TaskExecuteStep.DeviceType.Value, tunnle, region.Code);
            var pos = location.Split("_");
            if (pos.Length > 3)
            {
                var getLocation =
                    request.WcsTask.GetLocation = _locationService.AnalysisGetLocation(location);
                @bool = true;
            }
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{request.WcsTask.SerialNumber}--巷道获取失败");
        }

        return @bool;
    }
}