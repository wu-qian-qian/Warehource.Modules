using Wcs.Device.Device.Tranship;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceData;

public class StackerOutTranShip : AbstrraStackerTranship
{
    public StackerOutTranShip(string name, StackerTranShipConfig config) : base(name, config)
    {
    }
}