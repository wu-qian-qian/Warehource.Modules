using Wcs.Contracts.Respon.Device;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceStructure.Tranship;

public class StackerOutTranShip : AbstractStackerTranship
{
    public StackerOutTranShip(StackerTranShipConfig config, DeviceDto device) : base(device.DeviceName, config,
        device.RegionCode,
        device.Enable, device.GroupName)
    {
        DeviceType = DeviceTypeEnum.StackerOutTranShip;
    }
}