using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

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
    }

    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new UserProfile());
    }
}