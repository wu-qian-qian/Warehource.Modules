using System.Reflection;
using AutoMapper;
using Common.Infrastructure.EF;
using Common.Infrastructure.Event;
using Common.Infrastructure.MediatR;
using MassTransit;

namespace Warehource.Source;

public static class AddModulesExtensions
{
    public static IServiceCollection AddModules(this IServiceCollection service,
        IConfiguration configuration,
        Assembly[] assemblies,
        Action<IServiceCollection, IConfiguration>[] infrastructureModules,
        Action<MediatRServiceConfiguration>[] mediatrActions,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        Action<IServiceCollection>[] jobActs,
        Action<IMapperConfigurationExpression>[] mapConfigs)
    {
        AddInfrastructureModules(service, configuration, infrastructureModules);
        AddMassTransit(service, moduleConfigureConsumers);
        AddEvent(service, assemblies);
        AddMediatR(service, assemblies, mediatrActions);
        AddJob(service, jobActs);
        AddAutoMap(service, mapConfigs);
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