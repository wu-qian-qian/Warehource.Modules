using Common.Domain.Event;

namespace Wcs.Application.Custom;

public sealed class SendWmsTasksIntegrationEvent : IMassTransitDomainEvent
{
    public SendWmsTasksIntegrationEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
}