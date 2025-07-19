namespace Common.Application.QuartzJob;

/// <summary>
///     超时字段名
/// </summary>
public static class JobConstTitle
{
    public const string TimeoutSeconds = "TimeoutSeconds";
}

public record QuartzJobOptins(int TimeOut = 5)
{
    public string TimeoutSeconds { get; } = JobConstTitle.TimeoutSeconds;
}