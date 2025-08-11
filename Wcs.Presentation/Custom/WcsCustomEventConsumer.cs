using Common.Domain.Event;
using MassTransit;

namespace Wcs.Presentation.Custom;

public class WcsCustomEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        //context.Publish
        return Task.CompletedTask;
    }
}