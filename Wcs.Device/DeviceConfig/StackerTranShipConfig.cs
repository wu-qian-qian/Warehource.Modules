using Wcs.Device.Abstract;

namespace Wcs.Device.DeviceConfig;

public class StackerTranShipConfig : BaseDeviceConfig
{
    public string PipelinCode { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int Floor { get; set; }

    public int Tunnle { get; set; }
}