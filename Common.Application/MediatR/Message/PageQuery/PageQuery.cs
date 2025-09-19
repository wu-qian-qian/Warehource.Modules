using Common.Shared;

namespace Common.Application.MediatR.Message.PageQuery;

public abstract class PageQuery<TData> : IQuery<PageResult<TData>>
{
    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    /// <summary>
    ///     数量
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    ///     页
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    ///     跳过数量
    /// </summary>
    public int SkipCount => (PageIndex - 1) * Total;
}