using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Shared;

namespace Wcs.Application.DBHandler.ExecueNode.Get;

public class GetExecuteNodeQuery : IQuery<Result<IEnumerable<ExecuteNodeDto>>>
{
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
}