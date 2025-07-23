using Common.Domain.Event;

namespace Plc.CustomEvents;

public class PlcIntegrationEvent : IMassTransitDomainEvent
{
    public PlcIntegrationEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
}