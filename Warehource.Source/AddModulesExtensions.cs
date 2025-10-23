using System.Reflection;
using AutoMapper;
using Common.Infrastructure.EF;
using Common.Infrastructure.Event;
using Common.Infrastructure.MediatR;
using Common.Infrastructure.StateMachine;
using MassTransit;

namespace Warehource.Source;

public static class AddModulesExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="configuration">配置</param>
    /// <param name="assemblies">程序集</param>
    /// <param name="infrastructureModules">模块的基础设施</param>
    /// <param name="mediatrActions">mediatr中介者</param>
    /// <param name="moduleConfigureConsumers">模块通讯消费者</param>
    /// <param name="jobActs">后台任务</param>
    /// <param name="mapConfigs">automapper</param>
    /// <returns></returns>
    public static IServiceCollection AddModules(this IServiceCollection service,
        IConfiguration configuration,
        Assembly[] assemblies,
        Action<IServiceCollection, IConfiguration>[] infrastructureModules,
        Action<MediatRServiceConfiguration>[] mediatrActions,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        Action<IServiceCollection>[] jobActs,
        Action<IMapperConfigurationExpression>[] mapConfigs)
    {
        //基础设施模块注入
        AddInfrastructureModules(service, configuration, infrastructureModules);
        //模块masstransit事件消费者
        AddMassTransit(service, moduleConfigureConsumers);
        //本地事件总线
        AddEvent(service, assemblies);
        //中介者
        AddMediatR(service, assemblies, mediatrActions);
        //后台任务
        AddJob(service, jobActs);
        // automapper
        AddAutoMap(service, mapConfigs);
        //数据库拦截器
        service.AddScoped<LastModificationInterceptor>();
        return service;
    }

    internal static IServiceCollection AddInfrastructureModules(IServiceCollection services,
        IConfiguration configuration,
        Action<IServiceCollection, IConfiguration>[] infrastructureModules)
    {
        foreach (var item in infrastructureModules) item(services, configuration);

        return services;
    }

    /// <summary>
    ///     MassTransit 模块注册
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static IServiceCollection AddMassTransit(this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddMassTransitEventBus(moduleConfigureConsumers);
        return services;
    }

    /// <summary>
    ///     本地事件总线注册
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modelusAss"></param>
    /// <returns></returns>
    internal static IServiceCollection AddEvent(this IServiceCollection services, Assembly[] modelusAss)
    {
        services.AddEventExtensionConfiguator(modelusAss);
        return services;
    }

    internal static IServiceCollection AddState(this IServiceCollection services, Assembly[] modelusAss)
    {
        services.AddStateMachineExtensionConfiguator(modelusAss);
        return services;
    }

    /// <summary>
    ///     MediatR
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modelusAss"></param>
    /// <returns></returns>
    internal static IServiceCollection AddMediatR(this IServiceCollection services, Assembly[] modelusAss
        , Action<MediatRServiceConfiguration>[] behaviorActs)
    {
        services.AddMediatRConfigurator(modelusAss, behaviorActs);
        return services;
    }

    internal static IServiceCollection AddJob(this IServiceCollection services, Action<IServiceCollection>[] jobs)
    {
        foreach (var item in jobs) item(services);
        return services;
    }

    internal static IServiceCollection AddAutoMap(this IServiceCollection services,
        Action<IMapperConfigurationExpression>[] mapConfigs)
    {
        var auto = new MapperConfiguration(config =>
        {
            foreach (var item in mapConfigs) item.Invoke(config);
        });
        services.AddSingleton(auto.CreateMapper());
        return services;
    }
}