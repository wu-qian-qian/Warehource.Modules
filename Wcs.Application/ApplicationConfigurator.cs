using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.DeviceExecute.Behaviors;

namespace Wcs.Application;

/// <summary>
///     一些注入配置
/// </summary>
public static class ApplicationConfigurator
{
    public static void AddMediatR(MediatRServiceConfiguration configuration)
    {
        configuration.AddOpenBehavior(typeof(CanBusinessPipelinBehavior<,>));
    }


    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new WcsProfile());
    }
}