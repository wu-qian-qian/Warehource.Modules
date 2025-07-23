using Plc.Shared;

namespace Plc.Presentation.S7Plc;

internal record S7NetRequest
{
    /// <summary>
    ///     PLC 地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    ///     端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    ///     PLC 类型
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
}