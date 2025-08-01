using Common.Domain.Event;

namespace Plc.CustomEvents;

/// <summary>
///     缓存方式
///     2种方式
///     ip统一读取   只需要有IP地址即可
///     通过设备名称读取 需要ip地址和设备名称
///     设备名+变量名读取 需要ip地址和设备名和变量名
///     3种读取模式
/// </summary>
public class S7ReadPlcDataBlockEvent : IMassTransitDomainEvent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="occurredOnUtc"></param>
    /// <param name="id">通过id标识</param>
    public S7ReadPlcDataBlockEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {

    }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    ///     变量名称
    /// </summary>
    public IEnumerable<string> DBNames { get; set; }

    public string Ip { get; set; }

    /// <summary>
    ///     是否使用缓存
    ///     这样可以减少对数据库的访问
    /// </summary>
    public bool UseMemory { get; set; } = true;

    /// <summary>
    ///     是否为批量读取
    ///     非批处理需要唯一id来获取缓存中的变量
    ///     批处理可通过唯一id，或者设备好，ip
    /// </summary>
    public bool IsBath
    {
        get
        {
            if (DBNames != null && DBNames.Any())
                return false;
            return true;
        }
    }
}