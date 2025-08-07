using System.Diagnostics;
using Common.Application.Log;
using Common.Shared;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Middleware;

public class GlobalLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Stopwatch _stopwatch;

    public GlobalLogMiddleware(RequestDelegate next)
    {
        _next = next;
        _stopwatch = new Stopwatch();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _stopwatch.Restart();
        var request = context.Request;
        // 获取Response.Body内容
        var originalBodyStream = context.Response.Body;
        var responseData = string.Empty;
        var requestData = string.Empty;
        //可以让 Request.Body 可以再次读取
        if (request.ContentLength != null)
        {
            // 允许请求体被多次读取
            context.Request.EnableBuffering();

            // 读取请求体内容
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                requestData = await reader.ReadToEndAsync();
                // 重置流的位置，让后续中间件能正常读取
                context.Request.Body.Position = 0;
            }
        }

        var contentType = context.Response.ContentType;
        var isFileResponse = MiddlewareHelper.IsFileResponse(contentType);
        if (isFileResponse == false)
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                responseData = await GetResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }

        _stopwatch.Stop();
        Serilog.Log.Logger.ForCategory(LogCategory.Http)
            .Information($"地址：{context.Connection.RemoteIpAddress.ToString()}\nURL:{context.Request.Path}\n请求体：{requestData}\n响应体：{responseData}\n时间：{_stopwatch.ElapsedMilliseconds}");
    }

    /// <summary>
    ///     获取响应内容
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private async ValueTask<string> GetResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }
}