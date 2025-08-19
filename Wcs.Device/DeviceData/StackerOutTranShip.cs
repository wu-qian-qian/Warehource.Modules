using Wcs.Device.Device.Tranship;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceData;

public class StackerOutTranShip : AbstractStackerTranship
{
    public StackerOutTranShip(StackerTranShipConfig config, string name, string regionCodes, bool enable) : base(config,
        name, regionCodes, enable)
    {
        DeviceType = DeviceTypeEnum.StackerOutTranShip;
    }
}