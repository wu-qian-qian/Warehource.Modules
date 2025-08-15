using Common.Domain.Event;

namespace Wcs.CustomEvents;

/// <summary>
///     变量映射事件
/// </summary>
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