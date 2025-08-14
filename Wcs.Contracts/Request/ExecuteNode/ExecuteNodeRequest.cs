using Wcs.Shared;

namespace Wcs.Contracts.Request.ExecuteNode;

public class ExecuteNodeRequest
{
    /// <summary>
    /// </summary>
    public Guid? Id { get; set; }

    public byte Index { get; set; }

    /// <summary>
    ///     路径组
    /// </summary>
    public string? PahtNodeGroup { get; set; }

    /// <summary>
    ///     当前设备类型
    /// </summary>
    public DeviceTypeEnum? CurrentDeviceType { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public WcsTaskTypeEnum? TaskType { get; set; }

    public string RegionCode { get; set; }
}