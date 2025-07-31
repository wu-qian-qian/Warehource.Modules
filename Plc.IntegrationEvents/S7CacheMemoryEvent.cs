using Common.Domain.Event;

namespace Plc.CustomEvents;

/// <summary>
///     缓存方式
///     2种方式
///     
/// ip统一读取   只需要有IP地址即可
/// 通过设备名称读取 需要ip地址和设备名称
/// 设备名+变量名读取 需要ip地址和设备名和变量名
/// 3种读取模式
/// </summary>
public class S7CacheMemoryEvent : IMassTransitDomainEvent
{
    public S7CacheMemoryEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// 变量名称
    /// </summary>
    public IEnumerable<string> DBNames { get; set; }
    public string Ip { get; set; }

    /// <summary>
    ///     是否使用缓存
    ///     这样可以减少对数据库的访问
    /// </summary>
    public bool UserMemory { get; set; } = true;

    /// <summary>
    /// 是否为批量读取
    /// </summary>
    public bool IsBath
    {
        get
        {
            if (DBNames != null && DBNames.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}