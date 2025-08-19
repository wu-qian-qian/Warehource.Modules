using Wcs.Device.Device.Stacker;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceData;

/// <summary>
///     堆垛机数据结构
/// </summary>
public class Stacker : AbstractStacker
{
    public Stacker(string name, StackerConfig config, string regionCode, bool enable) : base(name, config, regionCode,
        enable)
    {
        DeviceType = DeviceTypeEnum.Stacker;
    }
}