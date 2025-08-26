using Common.Domain.EF;

namespace Wcs.Domain.Region;

/// <summary>
///     区域表
///     区域实际上表示行走的路线
///     a,b,c 3个区域表示3条路径
///     设备中存在的区域编码表示在a路线中需要进行该设备(会包含多个区域编码)
///     执行存在的的区域表示该路径需要经过那些设备
///     所以关联关系为  通过任务信息来确定区域  通过区域可以获取到路线
///     每一设备执行可以知道下一个节点  这样形成了节点关联
///     这样每一个节点就只关联自己，
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