using Wcs.Device.Device.Tranship;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceData;

public class StackerInTranShip : AbstrraStackerTranship
{
    public StackerInTranShip(string name, StackerTranShipConfig config) : base(name, config)
    {
    }
}