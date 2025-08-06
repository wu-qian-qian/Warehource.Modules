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
    ///     路径组
    /// </summary>
    public string PahtNodeGroup { get; set; }

    /// <summary>
    ///     当前设备类型
    /// </summary>
    public DeviceTypeEnum CurrentDeviceType { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public WcsTaskTypeEnum TaskType { get; set; }

    /// <summary>
    ///     当前节点设备名
    /// </summary>
    public string CurrentDeviceName { get; set; }

    /// <summary>
    ///     下一节点设备名
    /// </summary>

    public string? NextDeviceName { get; set; }

    public Guid? RegionId { get; set; }

    public Region.Region Region { get; set; }
}