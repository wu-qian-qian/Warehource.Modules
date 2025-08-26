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

    public string DeviceRegionCode { get; set; }

    /// <summary>
    ///     是否获取下一节点所执行的设备
    /// </summary>
    public bool IsGetDeviceName { get; set; } = true;

    public string Title { get; set; }
}