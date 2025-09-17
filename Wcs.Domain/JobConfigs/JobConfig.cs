using Common.Domain.EF;

namespace Wcs.Domain.JobConfigs;

public class JobConfig : IEntity
{
    public JobConfig() : base(Guid.NewGuid())
    {
    }

    public string Name { get; set; }

    /// <summary>
    ///     描述，用来标识任务属于哪个类型执行
    /// </summary>
    public string? Description { get; set; }

    public string JobType { get; set; }

    public int TimeOut { get; set; }

    public string TimeoutSeconds { get; set; } = "TimeoutSeconds";

    public int Timer { get; set; }

    public bool IsStart { get; set; }

    public void SetStatus(bool isStart)
    {
        IsStart = isStart;
    }

    public void SetTimer(int timer)
    {
        Timer = timer;
    }

    public void SetTimerOut(int timerOut)
    {
        TimeOut = timerOut;
    }
}