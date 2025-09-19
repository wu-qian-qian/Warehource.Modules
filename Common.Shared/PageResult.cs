namespace Common.Shared;

public class PageResult<T>
{
    public PageResult(long totalCount, IReadOnlyList<T> data)
    {
        TotalCount = totalCount;
        Data = data;
    }

    /// <summary>
    ///     页面数据
    /// </summary>
    public IReadOnlyList<T> Data { get; set; }

    /// <summary>
    ///     总行数
    /// </summary>
    public long TotalCount { get; set; }
}