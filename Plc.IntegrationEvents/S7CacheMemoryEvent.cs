using Common.Domain.Event;

namespace Plc.CustomEvents;

/// <summary>
///     缓存方式
///     2种方式
/// </summary>
public class S7CacheMemoryEvent : IMassTransitDomainEvent
{
    public S7CacheMemoryEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
    
    public string DeviceName { get; set; }
    
    public string Ip { get; set; }

    /// <summary>
    /// 是否使用缓存
    /// 这样可以减少对数据库的访问
    /// </summary>
    public bool UserMemory { get; set; } = true;
}