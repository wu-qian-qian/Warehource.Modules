namespace Common.Infrastructure.Middleware;

public class MiddlewareHelper
{
    public static bool IsFileResponse(string contentType)
    {
        var isFileResponse = contentType != null && (
            contentType.StartsWith("application/octet-stream") ||
            contentType.StartsWith("application/pdf") ||
            contentType.StartsWith("image/") ||
            contentType.StartsWith("application/vnd.")
        );
        return isFileResponse;
    }
}