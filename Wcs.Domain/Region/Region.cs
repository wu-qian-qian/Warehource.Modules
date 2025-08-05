using Common.Domain.EF;

namespace Wcs.Domain.Region;

/// <summary>
///     区域表
/// </summary>
public class Region : IEntity
{
    public Region() : base(Guid.NewGuid())
    {
    }

    public string Code { get; set; }

    public string? Description { get; set; }
    
    /// <summary>
    /// 当前区域任务进行中数量
    /// 用来限流
    /// </summary>
    public int? CurrentNum { get; set; }
}