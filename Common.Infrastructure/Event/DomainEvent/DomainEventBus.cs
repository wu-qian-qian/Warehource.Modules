using Common.Application.Event;
using Common.Domain.Event;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Event.DomainEvent;

/// <summary>
///     本地事件总线实现
/// </summary>
internal class DomainEventBus : IDomainEventBus
{
    //  IServiceScopeFactory serviceScopeFactory
    private readonly EventManager _eventManager;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventBus(EventManager eventManager, IServiceScopeFactory serviceScopeFactory)
    {
        _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        _serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IEventDomain
    {
        var eventName = integrationEvent.GetType().FullName ??
                        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
        if (_eventManager.HasSubscriptionsForEvent(eventName))
        {
            var handels = _eventManager.GetHandlerForEvent(eventName);
            foreach (var item in handels)
            {
                var handler = _serviceProvider.GetService(item);
                if (handler is IDomainEventHandler domainEventHandler)
                    await domainEventHandler.Handle(integrationEvent, cancellationToken);
            }
        }
    }
}