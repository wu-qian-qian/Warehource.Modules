using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Business.CheckExecuteNode;

/// <summary>
///     用来检测更新下一节点，和下一执行设备
/// </summary>
public class CheckExecuteNodeCommand : ICommand<Result<bool>>
{
    public WcsTask WcsTask { get; set; }

    /// <summary>
    ///     设备行驶区域
    /// </summary>
    public string DeviceRegionCode { get; set; }

    /// <summary>
    ///     因为有一些设备涉及逻辑动态分配无法进行明确的指定
    /// </summary>
    public bool IsGetNextNode { get; set; } = true;

    public string Title { get; set; }
}