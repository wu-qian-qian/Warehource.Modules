using Common.Application.Caching;
using Common.Application.Event;
using Common.Domain.Event;
using MassTransit;
using MediatR;
using Plc.Application.S7Plc.ReadPlc;
using Plc.CustomEvents;

namespace Plc.Application.Custom;

/// <summary>
///     2种策略
///     1、分布式缓存存储
///     2、公共事件触发
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
/// <param name="bus"></param>
/// <param name="cache"></param>
internal class ReadPlcEventConsumer<TIntegrationEvent>(IMassTransitEventBus bus,ISender sender)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        if (context.Message.EventHash == nameof(S7CacheMemoryEvent).GetHashCode())
        {
            if (context.Message is S7CacheMemoryEvent s7CacheMemoryEvent)
            {
               await sender.Send
                (
                    new PlcEventCommand
                    {
                        UseMemory = s7CacheMemoryEvent.UserMemory,
                        Ip = s7CacheMemoryEvent.Ip,
                        DeviceName = s7CacheMemoryEvent.DeviceName
                    }
                );
            }
        }
    }
}