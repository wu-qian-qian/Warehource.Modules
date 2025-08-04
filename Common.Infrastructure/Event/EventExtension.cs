using System.Reflection;
using Common.Application.Event;
using Common.Application.Event.Local;
using Common.Domain.Event;
using Common.Infrastructure.Event.Local;
using Common.Infrastructure.Event.Masstransit;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Infrastructure.Event;

public static class EventExtension
{
    /// <summary>
    ///     注意实体事件需要和实体在同一个程序集下
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddEventExtensionConfiguator(this IServiceCollection services, Assembly[] assembly)
    {
        var handlerTypes = GetEventTypes(assembly, services);
        services.AddSingleton<ILocalEventBus>(sp =>
        {
            var eventManager = new EventManager();
            foreach (var item in handlerTypes) eventManager.AddSubscription(item.Key, item.Value);
            var serviceProvider = sp.GetRequiredService<IServiceProvider>();
            return new LocalLocalEventBus(eventManager, serviceProvider);
        });
        return services;
    }

    /// <summary>
    ///     获取事件类型和对应的处理器类型
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    internal static Dictionary<string, Type[]> GetEventTypes(Assembly[] assemblies, IServiceCollection descriptors)
    {
        if (assemblies == null || assemblies.Length == 0)
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        var dictionary = new Dictionary<string, Type[]>();
        foreach (var assembly in assemblies)
        {
            //获取所有实现了IEventDomain接口的类型
            var handlerDomains = assembly.GetTypes().Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                                                   type.IsAssignableTo(typeof(IEventDomain)));

            foreach (var item in handlerDomains)
            {
                //获取所有实现了IDomainEventHandler<item>接口的处理器类型
                var handlers = assembly.GetTypes()
                    .Where(t => t is { IsAbstract: false, IsInterface: false } &&
                                t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(item)))
                    .ToArray();
                foreach (var handler in handlers) descriptors.AddTransient(handler);
                dictionary.Add(item.FullName, handlers);
            }
        }

        return dictionary;
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
        services.TryAddSingleton<IMassTransitEventBus, MassTransitEventBus>();
        return services;
    }
}