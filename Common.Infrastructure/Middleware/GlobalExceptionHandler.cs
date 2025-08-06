using Common.Application.Log;
using Common.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        Serilog.Log.Logger.ForCategory(LogCategory.Error)
            .Error($"{exception.StackTrace}--{exception.InnerException.Message}");
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