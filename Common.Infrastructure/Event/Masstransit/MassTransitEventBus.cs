using Common.Application.Event;
using Common.Domain.Event;
using MassTransit;

namespace Common.Infrastructure.Event.Masstransit;

/// <summary>
///     MassTransit 事件总线实现 公共事件模块
/// </summary>
/// <param name="bus"></param>
internal class MassTransitEventBus(IBus bus) : IMassTransitEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IMassTransitDomainEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}