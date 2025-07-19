using Common.Application.Event;
using Common.Application.Event.Local;
using Common.Domain.Event;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Event.Local;

/// <summary>
///     本地事件总线实现
/// </summary>
internal class LocalLocalEventBus : ILocalEventBus
{
    //  IServiceScopeFactory serviceScopeFactory
    private readonly EventManager _eventManager;
    private readonly IServiceProvider _serviceProvider;

    public LocalLocalEventBus(EventManager eventManager, IServiceProvider serviceProvider)
    {
        _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IEventDomain
    {
        var eventName = integrationEvent.GetType().FullName ??
                        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
        if (_eventManager.HasSubscriptionsForEvent(eventName))
        {
            var handels = _eventManager.GetHandlerForEvent(eventName);
            var provider = _serviceProvider.CreateScope().ServiceProvider;
            foreach (var item in handels)
            {
                var handler = provider.GetService(item);
                if (handler is IDomainEventHandler domainEventHandler)
                    await domainEventHandler.Handle(integrationEvent, cancellationToken);
            }
        }
    }
}