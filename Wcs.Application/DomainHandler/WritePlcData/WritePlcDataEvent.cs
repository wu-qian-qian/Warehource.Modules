using Common.Domain.Event;

namespace Wcs.Application.DomainEvent.WritePlcData;

public class WritePlcDataEvent : IEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

    public string CacheKey { get; set; }

    public bool IsSuccess { get; set; }
}