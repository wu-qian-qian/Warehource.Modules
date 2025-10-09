using System.Collections.Concurrent;

namespace Common.Infrastructure.Event.DomainEvent;

internal class EventManager
{
    private static readonly ConcurrentDictionary<string, Type[]> _handlers = new();

    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }

    public IEnumerable<Type> GetHandlerForEvent(string eventName)
    {
        return _handlers[eventName];
    }

    /// <summary>
    ///     添加订阅
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="handler"></param>
    public void AddSubscription(string eventName, Type[] handlers)
    {
        if (!HasSubscriptionsForEvent(eventName)) _handlers.TryAdd(eventName, handlers);
    }
}