using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.GetNetNode;

/// <summary>
///     通过一些标识获取到下一节点的执行设备
/// </summary>
/// <param name="_deviceService"></param>
public class GetNextNodeCommandHandler(IDeviceService _deviceService)
    : ICommandHandler<GetNextNodeCommand, string>
{
    public async Task<string> Handle(GetNextNodeCommand request, CancellationToken cancellationToken)
    {
        var deviceName = string.Empty;
        switch (request.DeviceType)
        {
            case DeviceTypeEnum.StackerInTranShip:
            case DeviceTypeEnum.StackerOutTranShip:
            case DeviceTypeEnum.Stacker:
                deviceName =
                    await _deviceService.GetDeviceNameWithTunnleAsync(request.DeviceType, request.Filter,
                        request.RegionCode);
                break;
            case DeviceTypeEnum.StockPortOut:
            case DeviceTypeEnum.StockPortIn:
                deviceName =
                    await _deviceService.GetDeviceNameWithTargetCodeAsync(request.DeviceType, request.Filter,
                        request.RegionCode);
                break;
        }

        return deviceName;
    }
}