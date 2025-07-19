using System.Reflection;
using Common.Application.MediatR.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.MediatR;

public static class MediatRExtensions
{
    /// <summary>
    ///     统一的 MediatR 配置方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleAssemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRConfigurator(this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);
        return services;
    }

    /// <summary>
    ///     通过 Action 配置 MediatR
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleAssemblies"></param>
    /// <param name="mediatrAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRConfigurator(this IServiceCollection services,
        Assembly[] moduleAssemblies, Action<MediatRServiceConfiguration> mediatrAction)
    {
        services.AddMediatR(mediatrAction);
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);
        return services;
    }

    /// <summary>
    ///     通过 Action 配置 MediatR 和行为
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleAssemblies"></param>
    /// <param name="behaviors"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRConfigurator(this IServiceCollection services,
        Assembly[] moduleAssemblies, Action<MediatRServiceConfiguration>[] behaviors)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            foreach (var action in behaviors) action(config);
        });
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);
        return services;
    }
}