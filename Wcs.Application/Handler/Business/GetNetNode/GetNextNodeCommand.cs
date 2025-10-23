using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.GetNetNode;

/// <summary>
///     巷道获取下一节点
/// </summary>
public class GetNextNodeCommand : ICommand<string>
{
    public DeviceTypeEnum DeviceType { get; set; }

    /// <summary>
    ///     筛选标签，对于出入库口为输送，对于为堆垛机或是堆垛机接驳位为巷道
    /// </summary>
    public string Filter { get; set; }

    public string RegionCode { get; set; }
}