using Common.Domain.EF;
using Wcs.Shared;

namespace Wcs.Domain.ExecuteNode;

/// <summary>
///     节点执行路径
/// </summary>
public class ExecuteNodePath : IEntity
{
    public ExecuteNodePath() : base(Guid.NewGuid())
    {
    }

    /// <summary>
    ///     当前设备类型
    /// </summary>
    public DeviceTypeEnum CurrentDeviceType { get; set; }

    public string CurrentDeviceName { get; set; }

    public Guid? RegionId { get; set; }

    public Region.Region Region { get; set; }
}