using Wcs.Device.Device.Tranship;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceData;

public class StackerInTranShip : AbstractStackerTranship
{
    public StackerInTranShip(string name, StackerTranShipConfig config, string regionCodes, bool enable) : base(config,
        name, regionCodes, enable)
    {
        DeviceType = DeviceTypeEnum.StackerInTranShip;
    }
}