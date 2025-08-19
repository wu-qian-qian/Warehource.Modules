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
    public Device() : base(Guid.NewGuid())
    {
    }

    public DeviceTypeEnum DeviceType { get; set; }

    public string DeviceName { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     配置
    /// </summary>
    public string Config { get; set; }

    public bool Enable { get; set; }

    /// <summary>
    /// 区域编码
    /// 如a,b,c,d
    /// </summary>
    public string? RegionCode { get; set; }

    /// <summary>
    /// 设备组 ，如同巷道多小车，同巷道多堆垛机
    /// </summary>
    public string? GroupName { get; set; }
}