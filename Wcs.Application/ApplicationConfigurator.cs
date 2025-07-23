using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Custom;
using Wcs.CustomEvents;

namespace Wcs.Application;

/// <summary>
///     一些注入配置
/// </summary>
public static class ApplicationConfigurator
{
    public static void AddMediatR(MediatRServiceConfiguration configuration)
    {
    }

    public static void AddCustom(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<WcsCustomEventConsumer<WcsIntegrationEvent>>();
    }

    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new WcsProfile());
    }
}