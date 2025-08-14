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

    public byte Index { get; set; }

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


    public Guid? RegionId { get; set; }

    /// <summary>
    ///     区域字符组
    /// </summary>

    public Region.Region Region { get; set; }

    /// <summary>
    ///     是否启动
    /// </summary>
    public bool Enable { get; set; }
}