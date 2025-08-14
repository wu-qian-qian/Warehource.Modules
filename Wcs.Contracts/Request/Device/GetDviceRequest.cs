using Wcs.Shared;

namespace Wcs.Contracts.Request.Device;

public sealed class GetDviceRequest
{
    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }

    public bool? Enable { get; set; }
}