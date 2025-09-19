using Wcs.Shared;

namespace Wcs.Contracts.Request.Device;

public sealed record GetDviceRequest(DeviceTypeEnum? DeviceType, string? DeviceName, bool? Enable);