using Common.Domain.Event;

namespace Plc.CustomEvents;

/// <summary>
///     缓存方式
///     2种方式
///     
/// 读取plc方式 2中方式ip统一读取 
/// 通过设备名称读取
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

    public string Ip { get; set; }

    /// <summary>
    ///     是否使用缓存
    ///     这样可以减少对数据库的访问
    /// </summary>
    public bool UserMemory { get; set; } = true;
}