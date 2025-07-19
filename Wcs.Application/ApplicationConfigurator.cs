using AutoMapper;
using Common.Application.Event.Custom;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Custom;
using Wcs.Application.S7Plc.Behaviors;

namespace Wcs.Application;

/// <summary>
///     一些注入配置
/// </summary>
public static class ApplicationConfigurator
{
    public static void AddMediatR(MediatRServiceConfiguration configuration)
    {
        configuration.AddBehavior<ReadS7PlcPipelineBehavior>();
    }

    public static void AddCustom(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<SendWmsTasksIntegrationEvent>>();
    }

    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new WcsProfile());
    }
}