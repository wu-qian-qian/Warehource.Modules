using Common.Domain.Event;

namespace Wcs.CustomEvents;

public class WcsIntegrationEvent : IMassTransitDomainEvent
{
    public WcsIntegrationEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
}