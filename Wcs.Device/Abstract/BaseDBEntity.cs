namespace Wcs.Device.Abstract;

public interface BaseDBEntity
{
    /// <summary>
    ///     是否为最新读取数据
    /// </summary>
    public bool IsRead { get; set; }
}