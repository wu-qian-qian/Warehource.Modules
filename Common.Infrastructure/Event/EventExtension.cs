using System.Reflection;
using Common.Application.Event;
using Common.Domain.Event;
using Common.Infrastructure.Event.DomainEvent;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Event;

public static class EventExtension
{
    /// <summary>
    ///     注意实体事件需要和实体在最好在同一个程序集下
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddEventExtensionConfiguator(this IServiceCollection services, Assembly[] assembly)
    {
        var eventManager = GetEventTypes(assembly, services);
        services.AddSingleton<IEventBus>(sp =>
        {
            var serviceScope = sp.GetRequiredService<IServiceScopeFactory>();
            return new EventBus(eventManager, serviceScope);
        });
        return services;
    }

    /// <summary>
    ///     获取事件类型和对应的处理器类型
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static EventManager GetEventTypes(Assembly[] assemblies, IServiceCollection descriptors)
    {
        if (assemblies == null || assemblies.Length == 0)
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        //程序集的所有类
        var assTypeList = assemblies.Select(p => p.GetTypes());
        var allTypeList = new List<Type>();
        foreach (var item in assTypeList) allTypeList.AddRange(item);
        //筛选出所有的事件类型

        #region 开启型泛型注入

        var handlerTypes = allTypeList.Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IEventHandler<,>))).ToArray();

        // 构建事件类型 -> 处理器类型列表 的映射
        var eventToHandlers = new Dictionary<string, Type>();

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<,>));

            if (interfaces.Any() == false || interfaces.Count() > 1)
            {
                throw new ArgumentException($"{handlerType.FullName} 必须实现且只能实现一个 IEventHandler<TEvent, TResponse> 接口");
            }

            // 获取事件类型 IEventHandler<,> 第一个泛型参数 IEvent 第二个返回类型 TResponse
            var eventType = interfaces.First().GetGenericArguments()[0];
            if (!eventToHandlers.ContainsKey(eventType.FullName))
            {
                eventToHandlers[eventType.FullName] = handlerType;
                descriptors.AddTransient(handlerType);
            }
        }

        #endregion

        var eventManager = new EventManager(eventToHandlers);

        #region 关闭型泛型注入

        var handlerDomains = allTypeList.Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                                       type.IsAssignableTo(typeof(IEvent)));
        foreach (var item in handlerDomains)
        {
            //获取所有实现了IDomainEventHandler<item>接口的处理器类型
            var handlers = allTypeList
                .Where(t => t is { IsAbstract: false, IsInterface: false } &&
                            t.IsAssignableTo(typeof(IEventHandler<>).MakeGenericType(item)))
                .ToArray();
            if (handlers.Length == 0 || handlers.Length > 1)
            {
                continue;
            }

            descriptors.AddTransient(handlers[0]);
            eventManager.AddSubscription(item.FullName, handlers[0]);
        }

        #endregion

        return eventManager;
    }

    public static bool ImplementsOpenGenericInterface(Type type, Type openGenericInterface)
    {
        if (!openGenericInterface.IsInterface || !openGenericInterface.IsGenericTypeDefinition)
            throw new ArgumentException("Must be an open generic interface", nameof(openGenericInterface));

        return type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericInterface);
    }

    /// <summary>
    ///     MassTransit 事件总线注册
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMassTransitEventBus(this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddMassTransit(busConfigurator =>
        {
            foreach (var item in moduleConfigureConsumers) item(busConfigurator);
            busConfigurator.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

            // 使用内存作为消息传输
        });
        return services;
    }
}