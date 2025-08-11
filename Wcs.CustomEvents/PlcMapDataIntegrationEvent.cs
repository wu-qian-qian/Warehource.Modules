using Common.Domain.Event;

namespace Wcs.CustomEvents;

public class PlcMapDataIntegrationEvent : IMassTransitDomainEvent
{
    public PlcMapDataIntegrationEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }

    public PlcMapDataIntegrationEvent() : base(DateTime.Now)
    {
    }

    public PlcMapDataIntegrationEvent(DateTime occurredOnUtc, string deviceName, string[] plcEntityName) : base(
        occurredOnUtc)
    {
        DeviceName = deviceName;
        PlcEntityName = plcEntityName;
    }

    public string DeviceName { get; set; }

    public string[] PlcEntityName { get; set; }
}