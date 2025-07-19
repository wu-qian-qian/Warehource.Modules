using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Middleware;

/// <summary>
///     Global exception handler that logs unhandled exceptions and returns a standardized ProblemDetails response.
///     全局异常处理程序，记录未处理的异常并返回标准化的 ProblemDetails 响应。
/// </summary>
/// <param name="logger"></param>
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "Server failure"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}

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