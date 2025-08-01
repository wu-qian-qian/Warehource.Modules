using Common.Domain.EF;
using Wcs.Shared;

namespace Wcs.Domain.Device;

/// <summary>
///     设备表
///     或者这
///     Config 为设备独立的配置 json格式
/// </summary>
public class Device : IEntity
{
    public Device(Guid id) : base(id)
    {
    }

    public DeviceTypeEnum DeviceType { get; set; }

    public string DeviceName { get; set; }

    public string Config { get; set; }

    public bool Enable { get; set; }
}