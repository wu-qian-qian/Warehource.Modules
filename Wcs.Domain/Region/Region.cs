using Common.Domain.EF;

namespace Wcs.Domain.Region;

/// <summary>
///     区域表
///     堆垛机、2向穿梭车对应的是巷道
///     一般以区域的分组维度进行调度
///     区域只要是适配行走的路线，其中路线行走的设备组
/// </summary>
public class Region : IEntity
{
    public Region() : base(Guid.NewGuid())
    {
    }

    public string Code { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     当前区域任务进行中数量
    ///     用来限流
    /// </summary>
    public int? CurrentNum { get; set; }
}