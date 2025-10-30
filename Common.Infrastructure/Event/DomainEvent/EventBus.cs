using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.Event;
using Common.Domain.State;
using MassTransit;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Common.Infrastructure.Event.DomainEvent;

/// <summary>
///     本地事件总线实现
/// </summary>
internal class EventBus : IEventBus
{
    private readonly EventManager _eventManager;
    private readonly IServiceProvider _serviceProvider;

    public EventBus(EventManager eventManager, IServiceScopeFactory serviceScopeFactory)
    {
        _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        _serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
    }

    [Obsolete("内部使用dynamic不推荐使用")]
    public async Task<TResponse> PublishAsync2<TResponse>(IEvent<TResponse> integrationEvent,
        CancellationToken cancellationToken = default)
    {
        var eventName = integrationEvent.GetType().FullName ??
                        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
        if (_eventManager.HasSubscriptionsForEvent(eventName))
        {
            var handels = _eventManager.GetHandlerForEvent(eventName);
            var handler = _serviceProvider.GetService(handels);
            try
            {
                dynamic dynHandler = handler;
                dynamic dynEvent = integrationEvent;
                return await dynHandler.Handle(dynEvent, cancellationToken);
            }
            catch (RuntimeBinderException ex)
            {
                Serilog.Log.Logger.ForCategory(Shared.LogCategory.Event)
                    .Information(eventName + " 事件处理器绑定失败:" + ex.Message);
            }
        }

        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
    }

    /// <summary>
    ///   发布事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="integrationEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IEvent
    {
        var eventName = integrationEvent.GetType().FullName ??
                        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
        if (_eventManager.HasSubscriptionsForEvent(eventName))
        {
            var handel = _eventManager.GetHandlerForEvent(eventName);
            var scop = _serviceProvider.CreateScope();
            var handler = scop.ServiceProvider.GetService(handel) as IStateMachine;
            if (handler is IEventHandler<T> domainEventHandler)
            {
                await domainEventHandler.Handle(integrationEvent, cancellationToken);
            }
        }
    }

    /// <summary>
    ///    发布事件并获取响应
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="event"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<TResponse> PublishAsync<TResponse>(
        IEvent<TResponse> @event,
        CancellationToken ct = default)
    {
        var eventType = @event.GetType();

        if (!_eventManager.TryGetHandler(eventType.FullName, out var handlerType))
            throw new InvalidOperationException($"No handlers registered for event: {eventType}");

        TResponse result = default!;
        var hasResult = false;

        var handlerDelegate =
            _eventManager.GetOrAddHandlerDelegate<TResponse>(eventType, handlerType, typeof(TResponse));

        // 2. 从 DI 解析处理器
        var handler = _serviceProvider.GetRequiredService(handlerType);

        // 3. 直接调用（无反射！）
        var task = handlerDelegate(handler, @event, ct);
        var response = await task.ConfigureAwait(false);

        // 假设只取第一个结果（或合并逻辑）
        if (!hasResult)
        {
            result = response;
            hasResult = true;
        }

        if (!hasResult)
            throw new InvalidOperationException("Handler returned no result.");

        return result;
    }
}