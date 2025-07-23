namespace Common.Application.QuartzJob;

public record QuartzJobOptins(int TimeOut = 5)
{
    public string TimeoutSeconds { get; } = JobConstTitle.TimeoutSeconds;
}