namespace Common.Domain.Response;

public record ApiResponse<T>(
    int Code,
    string Message,
    T? Data)
{
    public static ApiResponse<T> CreatApiResponse(int code, string message, T? data = default)
    {
        return new ApiResponse<T>(code, message, data);
    }
}