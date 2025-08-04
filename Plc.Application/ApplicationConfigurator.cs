using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Plc.Application.Behaviors.Read;
using Plc.Application.Behaviors.Write;
using Plc.Application.Custom;
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
        configuration.AddOpenBehavior(typeof(BathReadS7PlcPipelineBehavior<,>));
        configuration.AddOpenBehavior(typeof(SingleReadS7PlcPipelineBehavior<,>));
        configuration.AddOpenBehavior(typeof(FilterReadS7PlcPipelineBehavior<,>));
        configuration.AddOpenBehavior(typeof(WriteDtoInitPipelineBehavior<,>));
    }

    public static void AddCustom(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<ReadPlcEventConsumer<S7ReadPlcDataBlockEvent>>();
        registrationConfigurator.AddConsumer<WritePlcEventConsumer<S7WritePlcDataBlockEvent>>();
    }

    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new PlcProfile());
    }
}