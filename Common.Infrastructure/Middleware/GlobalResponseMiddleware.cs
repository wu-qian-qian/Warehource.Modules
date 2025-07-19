using System.Text.Json;
using Common.Domain.Response;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Middleware;

/// <summary>
///     构建统一返回值
/// </summary>
public class GlobalResponseMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 备份原始响应流
        var originalBodyStream = context.Response.Body;
        // 使用新的 MemoryStream 暂存响应
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        memoryStream.Seek(0, SeekOrigin.Begin);
        try
        {
            // 执行后续中间件/端点
            await _next(context);
            // 检查响应状态码和内容类型
            var contentType = context.Response.ContentType;
            var isFileResponse = contentType != null && (
                contentType.StartsWith("application/octet-stream") ||
                contentType.StartsWith("application/pdf") ||
                contentType.StartsWith("image/") ||
                contentType.StartsWith("application/vnd.")
            );
            //非文件类型进行统一封装
            if (isFileResponse == false)
            {
                var message = context.Response.StatusCode switch
                {
                    200 => "Success",
                    400 => "Bad Request",
                    401 => "Unauthorized",
                    403 => "Forbidden",
                    404 => "Not Found",
                    500 => "Internal Server Error",
                    _ => "Unknown Error"
                };
                memoryStream.Seek(0, SeekOrigin.Begin);
                var rawResponse = await new StreamReader(memoryStream).ReadToEndAsync();
                ApiResponse<object> apiResponse = default;
                //如果是json就进行统一的包装
                if (contentType != null && contentType.StartsWith("application/json"))
                {
                    var data = rawResponse != string.Empty ? JsonSerializer.Deserialize<object>(rawResponse) : null;
                    apiResponse = ApiResponse<object>.CreatApiResponse(context.Response.StatusCode, message, data);
                    memoryStream.SetLength(0);
                    await context.Response.WriteAsJsonAsync(apiResponse);
                }
                //else
                //{
                //    apiResponse = ApiResponse<object>.CreatApiResponse(context.Response.StatusCode, message, rawResponse);
                //    memoryStream.SetLength(0);
                //    await context.Response.WriteAsJsonAsync(apiResponse);
                //}
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }
}