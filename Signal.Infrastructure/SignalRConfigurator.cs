using Common.Presentation.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Application;
using SignalR.Infrastructure;
using SignalR.Presentation;

namespace Signal.Infrastructure;

public static class SignalRConfigurator
{
    public static IServiceCollection AddSignalRConfiguration(this IServiceCollection services)
    {
        // 注册SignalR服务
        services.AddSignalR();
        // 可以在这里添加其他SignalR相关的配置或服务
        services.AddEndPoint();
        services.AddScoped<IHubManager, HubManager>();
        return services;
    }

    public static IServiceCollection AddEndPoint(this IServiceCollection services)
    {
        return services.AddEndpoints(AssemblyReference.Assembly);
    }
}