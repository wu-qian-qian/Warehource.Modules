using Wcs.Device.Device.Stacker;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceData;

/// <summary>
///     堆垛机数据结构
/// </summary>
public class Stacker : AbstractStacker
{
    public Stacker(string name, StackerConfig config)
    {
        Name = name;
        Config = config;
    }
}