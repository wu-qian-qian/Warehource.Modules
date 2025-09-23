using Common.Domain.Net;
using Plc.Shared;

namespace Plc.Domain.S7;

public class S7NetConfig : INetEnitty
{
    public S7NetConfig() : base(Guid.NewGuid())
    {
    }

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

    public ICollection<S7EntityItem> S7EntityItems { get; set; }

    public void UpdateIp(string ip)
    {
        Ip = ip;
    }

    public void UpdatePort(int port)
    {
        Port = port;
    }
}