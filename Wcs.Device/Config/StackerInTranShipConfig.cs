using Wcs.Device.Abstract;

namespace Wcs.Device.Config;

public class StackerInTranShipConfig : BaseDeviceConfig
{
    public string PipelinCode { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int Floor { get; set; }

    public int Tunnle { get; set; }
}