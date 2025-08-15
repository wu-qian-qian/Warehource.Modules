using Wcs.Device.Abstract;

namespace Wcs.Device.Config;

public class StackerConfig : BaseDeviceConfig
{
    /// <summary>
    ///     巷道编码
    /// </summary>
    public int Tunnle { get; set; }
}