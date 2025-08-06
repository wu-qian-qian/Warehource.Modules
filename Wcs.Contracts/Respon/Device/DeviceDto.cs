using Wcs.Shared;

namespace Wcs.Contracts.Respon.Device;

public class DeviceDto : BaseDto
{
    public DeviceTypeEnum DeviceType { get; set; }

    public string DeviceName { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     配置
    /// </summary>
    public string Config { get; set; }

    public bool Enable { get; set; }
}