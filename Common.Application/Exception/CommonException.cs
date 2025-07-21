namespace Common.Application.Exception;


/// <summary>
/// 
/// </summary>
public sealed class CommonException : System.Exception
{
    public CommonException(string requestName, System.Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
    }

    public string RequestName { get; }
}