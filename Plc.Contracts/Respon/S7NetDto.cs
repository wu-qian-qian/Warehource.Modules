using Plc.Shared;

namespace Plc.Contracts.Respon;

public class S7NetDto
{
    public Guid Id { get; set; }

    /// <summary>
    ///     地址
    /// </summary>
    public string Ip { get; init; }

    /// <summary>
    ///     端口
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    ///     Plc类型
    /// </summary>
    public S7TypeEnum S7Type { get; set; }

    /// <summary>
    ///     槽号
    /// </summary>
    public short Solt { get; set; }

    /// <summary>
    ///     机架
    /// </summary>
    public short Rack { get; set; }

    /// <summary>
    ///     读取超时
    /// </summary>
    public int ReadTimeOut { get; set; }

    /// <summary>
    ///     写入超时
    /// </summary>
    public int WriteTimeOut { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool IsUse { get; set; }

    /// <summary>
    ///     读取心跳地址
    /// </summary>
    public string? ReadHeart { get; set; }

    /// <summary>
    ///     写入心跳地址
    /// </summary>
    public string? WriteHeart { get; set; }
}