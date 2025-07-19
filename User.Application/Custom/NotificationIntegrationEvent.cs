using Common.Domain.Event;

namespace User.Application.Custom;

public class NotificationIntegrationEvent : IMassTransitDomainEvent
{
    public NotificationIntegrationEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }
}