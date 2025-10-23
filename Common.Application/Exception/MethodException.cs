namespace Common.Application.Exception;

/// <summary>
/// </summary>
public sealed class MethodException : System.Exception
{
    public MethodException(string requestName, System.Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
    }

    public string RequestName { get; }
}