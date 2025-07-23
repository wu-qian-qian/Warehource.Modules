using Common.Domain.Event;
using MassTransit;

namespace Wcs.Application.Custom;

internal class WcsCustomEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        return Task.CompletedTask;
    }
}