using System.Diagnostics;
using System.Text.RegularExpressions;
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
        var request = context.Request;
        // 获取Response.Body内容

        var responseData = string.Empty;
        var requestData = string.Empty;
        var contentType = context.Response.ContentType;
        var isFileResponse = MiddlewareHelper.IsFileResponse(contentType);
        if (isFileResponse == false) //如果请求流不为文件
            using (var responseBody = new MemoryStream())
            {
                var containsGet = Regex.IsMatch(context.Request.Path, "get");
                if (request.Method != "Get" || containsGet) //get方法不做日志记录
                {
                    await _next(context);
                }
                else
                {
                    context.Response.Body = responseBody;
                    _stopwatch.Restart();
                    await _next(context);
                    _stopwatch.Stop();
                    var originalBodyStream = context.Response.Body;
                    //可以让 Request.Body 可以再次读取
                    if (request.ContentLength != null && request.ContentLength != 0)
                    {
                        // 允许请求体被多次读取
                        context.Request.EnableBuffering();

                        // 读取请求体内容
                        using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                        {
                            if (!request.ContentType.StartsWith("multipart/form-data",
                                    StringComparison.OrdinalIgnoreCase))
                                requestData = await reader.ReadToEndAsync();
                            // 重置流的位置，让后续中间件能正常读取
                            context.Request.Body.Position = 0;
                        }
                    }

                    responseData = await GetResponse(context.Response);
                    await responseBody.CopyToAsync(originalBodyStream);
                    Serilog.Log.Logger.ForCategory(LogCategory.Http)
                        .Information(
                            $"地址：{context.Connection.RemoteIpAddress.ToString()}\nURL:{context.Request.Path}\n请求体：{requestData}\n响应体：{responseData}\n时间：{_stopwatch.ElapsedMilliseconds}");
                }
            }
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