using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Plc.Application.Behaviors.Read;
using Plc.Application.Behaviors.Write;

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
        configuration.AddBehavior(typeof(BathReadS7PlcPipelineBehavior));
        configuration.AddBehavior(typeof(SingleReadS7PlcPipelineBehavior));
        configuration.AddBehavior(typeof(TransferReadS7PlcPipelineBehavior));
        configuration.AddOpenBehavior(typeof(WriteDtoInitPipelineBehavior<,>));
    }


    public static void AddAutoMapper(IMapperConfigurationExpression config)
    {
        config.AddProfile(new PlcProfile());
    }
}