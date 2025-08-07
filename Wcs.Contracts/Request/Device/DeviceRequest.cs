using Wcs.Shared;

namespace Wcs.Contracts.Request.Device;

public class DeviceRequest
{
    public Guid? Id { get; set; }

    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     配置
    /// </summary>
    public object? Config { get; set; }

    public bool? Enable { get; set; }
}