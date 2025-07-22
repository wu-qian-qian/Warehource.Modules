using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Middleware;

/// <summary>
///     TODO 对请求体进行全局编码处理
/// </summary>
public class GlobalEncodingRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public GlobalEncodingRequestMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //context.Request.EnableBuffering();

        //// 直接读取字节并转换为字符串
        //using (var memoryStream = new MemoryStream())
        //{
        //    await context.Request.Body.CopyToAsync(memoryStream);
        //    var jsonBody = Encoding.UTF8.GetString(memoryStream.ToArray());
        //    //对
        //    var bytes = Encoding.UTF8.GetBytes(jsonBody);
        //    var memory = new MemoryStream(bytes);
        //    context.Request.Body = memory;
        //    context.Request.Body.Position = 0; // 重置流位置
        //}

        await _next(context);
    }
}