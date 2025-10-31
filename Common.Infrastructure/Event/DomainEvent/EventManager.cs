using System.Collections.Concurrent;
using System.Linq.Expressions;
using Common.Application.Event;

namespace Common.Infrastructure.Event.DomainEvent;

internal sealed partial class EventManager
{
    private static readonly ConcurrentDictionary<string, Type> _handlers = new();


    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }

    public Type GetHandlerForEvent(string eventName)
    {
        return _handlers[eventName];
    }

    /// <summary>
    ///     添加订阅
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="handler"></param>
    public void AddSubscription(string eventName, Type handlers)
    {
        if (!HasSubscriptionsForEvent(eventName)) _handlers.TryAdd(eventName, handlers);
        else
            throw new AggregateException(eventName + " 已经存在订阅");
    }
}

internal sealed partial class EventManager
{
    // 注册时由 DI 注入所有处理器类型
    private readonly IReadOnlyDictionary<string, Type> _eventToHandlerTypes;

    private readonly ConcurrentDictionary<string, Delegate> _handlerDelegates = new();

    public EventManager(IReadOnlyDictionary<string, Type> eventToHandlerTypes)
    {
        _eventToHandlerTypes = eventToHandlerTypes;
    }

    public bool TryGetHandler(string eventName, out Type handler)
    {
        _eventToHandlerTypes.TryGetValue(eventName, out handler);
        if (handler != null)
            return true;
        return false;
    }

    public Func<object, object, CancellationToken, Task<TResponse>> GetOrAddHandlerDelegate<TResponse>(
        Type eventType,
        Type handlerType,
        Type responseType)
    {
        // 缓存键：(handlerType, eventType)
        var key = (handlerType, eventType);

        // 注意：这里需要为每个 TResponse 生成不同委托，所以用 responseType 参与 key
        // 但我们可以用非泛型委托 + object，再 cast —— 更简单的方式是下面的工厂

        // 更优：使用泛型工厂方法（见下方）
        var stringKey = $"{handlerType.FullName}-{eventType.FullName}";
        return (Func<object, object, CancellationToken, Task<TResponse>>)
            _handlerDelegates.GetOrAdd(stringKey, _ =>
            {
                // 构造接口类型：IEventHandler<eventType, responseType>
                var interfaceType = typeof(IEventHandler<,>)
                    .MakeGenericType(eventType, responseType);

                if (!interfaceType.IsAssignableFrom(handlerType))
                    throw new InvalidOperationException($"Handler {handlerType} does not implement {interfaceType}");

                var handleMethod = interfaceType.GetMethod("Handle")!;

                // 创建强类型委托
                var handlerParam = Expression.Parameter(typeof(object), "handler");
                var eventParam = Expression.Parameter(typeof(object), "event");
                var ctParam = Expression.Parameter(typeof(CancellationToken), "ct");

                var call = Expression.Call(
                    Expression.Convert(handlerParam, handlerType),
                    handleMethod,
                    Expression.Convert(eventParam, eventType),
                    ctParam
                );

                var lambda = Expression.Lambda<Func<object, object, CancellationToken, Task<TResponse>>>(
                    call, handlerParam, eventParam, ctParam);

                return lambda.Compile();
            });
    }
}