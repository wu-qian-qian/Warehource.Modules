using Common.Application.Caching;
using Common.Application.Event;
using Common.Domain.Event;
using MassTransit;

namespace Plc.Application.Custom;

/// <summary>
///     2种策略
///     1、分布式缓存存储
///     2、公共事件触发
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
/// <param name="bus"></param>
/// <param name="cache"></param>
internal class ReadPlcEventConsumer<TIntegrationEvent>(IMassTransitEventBus bus, ICacheService cache)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        Console.WriteLine("读取Plc数据");
    }
}