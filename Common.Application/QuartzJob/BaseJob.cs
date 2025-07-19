using Quartz;

namespace Common.Application.QuartzJob;

public abstract class BaseJob : IJob
{
    protected CancellationTokenSource _linkedCts;

    public virtual async Task Execute(IJobExecutionContext context)
    {
        // 创建链接取消令牌源
        _linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken);

        // 设置自定义超时
        var timeout = context.JobDetail.JobDataMap.GetInt(JobConstTitle.TimeoutSeconds);
        _linkedCts.CancelAfter(TimeSpan.FromSeconds(timeout));
    }
}