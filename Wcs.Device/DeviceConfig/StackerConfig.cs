using Wcs.Device.Abstract;

namespace Wcs.Device.DeviceConfig;

public class StackerConfig : BaseDeviceConfig
{
    /// <summary>
    ///     巷道编码
    /// </summary>
    public int Tunnle { get; set; }
}