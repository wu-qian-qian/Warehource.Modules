namespace Wcs.Contracts.Respon.ExecuteNode;

public class ExecuteNodeDto : BaseDto
{
    /// <summary>
    ///     路径组
    /// </summary>
    public string PahtNodeGroup { get; set; }

    /// <summary>
    ///     当前设备类型
    /// </summary>
    public string CurrentDeviceType { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public string TaskType { get; set; }

    /// <summary>
    ///     当前节点设备名
    /// </summary>
    public string CurrentDeviceName { get; set; }

    /// <summary>
    ///     下一节点设备名
    /// </summary>

    public string? NextDeviceName { get; set; }

    /// <summary>
    ///     区域描述
    /// </summary>
    public string RegionDescription { get; set; }

    /// <summary>
    ///     区域编码
    /// </summary>
    public string RegionCode { get; set; }
}