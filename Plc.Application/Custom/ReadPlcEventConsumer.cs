using Common.Application.Event;
using Common.Domain.Event;
using MassTransit;
using Plc.CustomEvents;

namespace Plc.Application.Custom;

internal class ReadPlcEventConsumer<TIntegrationEvent>(IMassTransitEventBus bus) : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        Console.WriteLine("去读取Plc数据");
    }
}