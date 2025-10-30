using Wcs.Device.Abstract;

namespace Wcs.Device.DeviceConfig;

public class StackerConfig : BaseDeviceConfig
{
    /// <summary>
    ///     巷道编码
    /// </summary>
    public string Tunnle { get; set; }

    public string StationColumn { get; set; }

    public string StationFloor { get; set; }

    public string StationRow { get; set; }
}