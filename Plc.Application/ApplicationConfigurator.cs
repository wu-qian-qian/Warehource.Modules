using AutoMapper;
using Common.Application.MediatR.Behaviors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Plc.Application.Custom;
using Plc.Application.S7Plc.Behaviors;
using Plc.CustomEvents;

namespace Plc.Application;

/// <summary>
///     一些注入配置
/// </summary>
public static class ApplicationConfigurator
{
    /// <summary>
    ///     独立管道配置
    /// </summary>
    /// <param name="configuration"></param>
    public static void AddMediatR(MediatRServiceConfiguration configuration)
    {
        configuration.AddOpenBehavior(typeof(ReadS7PlcPipelineBehavior<,>));
    }

    public static void AddCustom(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<ReadPlcEventConsumer<S7CacheMemoryEvent>>();
    }

    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new PlcProfile());
    }
}