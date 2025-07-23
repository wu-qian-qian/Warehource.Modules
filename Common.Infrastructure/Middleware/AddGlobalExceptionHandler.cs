using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Middleware;

public static class AddGlobalExceptionHandler
{
    /// <summary>
    ///     注入全局异常处理程序，记录未处理的异常并返回标准化的 ProblemDetails 响应。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IServiceCollection AddGlobalException(this IServiceCollection services)
    {
        // 注入全局异常监听
        services.AddExceptionHandler<GlobalExceptionHandler>();
        //ProblemDetails 是ASP.NET Core中用于处理API错误的标准化响应格式
        services.AddProblemDetails();
        return services;
    }

    public static WebApplication AddGlobalExceptionMiddleware(this WebApplication app)
    {
        app.UseExceptionHandler();
        return app;
    }
}