using Common.Shared;
using Wcs.Shared;

namespace Wcs.Contracts.Request.Device;

public class GetDevicePageRequest : PagingQuery
{
    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }

    public bool? Enable { get; set; }
}