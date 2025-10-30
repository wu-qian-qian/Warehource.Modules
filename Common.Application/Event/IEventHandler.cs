using Common.Domain.Event;
using MassTransit;

namespace Common.Application.Event;

/// <summary>
/// 事件处理器接口
/// </summary>
/// <typeparam name="TEvent"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IEventHandler<in TEvent, TResponse>
    where TEvent : IEvent<TResponse>
{
    ValueTask<TResponse> Handle(TEvent @event, CancellationToken cancellationToken = default);
}

/// <summary>
/// 事件处理器接口
/// </summary>
/// <typeparam name="TDomainEvent"></typeparam>
public interface IEventHandler<in TDomainEvent>
    where TDomainEvent : IEvent
{
    ValueTask Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}