using Common.Domain.Event;

namespace Plc.CustomEvents;

/// <summary>
///     缓存方式
/// </summary>
public class CacheMemoryEvent : IMassTransitDomainEvent
{
    public CacheMemoryEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
}