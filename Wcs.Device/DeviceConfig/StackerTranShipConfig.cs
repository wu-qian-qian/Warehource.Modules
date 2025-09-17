using Wcs.Device.Abstract;

namespace Wcs.Device.DeviceConfig;

public class StackerTranShipConfig : BaseDeviceConfig
{
    public string PipelinCode { get; set; }

    public string Row { get; set; }

    public string Column { get; set; }

    public string Floor { get; set; }

    public string Tunnle { get; set; }

    public string Depth { get; set; }
}