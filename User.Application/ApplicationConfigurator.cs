using AutoMapper;
using Common.Application.Event.Custom;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Custom;

namespace User.Application;

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
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<NotificationIntegrationEvent>>();
    }

    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new UserProfile());
    }
}