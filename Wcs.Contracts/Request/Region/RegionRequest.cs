namespace Wcs.Contracts.Request.Region;

public record RegionRequest(Guid? Id, string Code, string? Description)
{
    /// <summary>
    ///     当前区域任务进行中数量
    ///     用来限流
    ///     需要使用锁
    /// </summary>
    public int CurrentNum { get; set; }

    //最大流量
    public int MaxNum { get; set; }
}