using Wcs.Contracts.Respon.Device;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceStructure.Stacker;

/// <summary>
///     堆垛机数据结构
/// </summary>
public class Stacker : AbstractStacker
{
    public Stacker(StackerConfig config, DeviceDto device) : base(device.DeviceName, config, device.RegionCode,
        device.Enable, device.GroupName)
    {
        DeviceType = DeviceTypeEnum.Stacker;
    }
}