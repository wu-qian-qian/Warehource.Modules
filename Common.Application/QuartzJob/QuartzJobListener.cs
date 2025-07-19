using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Listener;

namespace Common.Application.QuartzJob;

public class QuartzJobListener(ILogger<QuartzJobListener> logger) : SchedulerListenerSupport
{
    public override Task SchedulerError(string msg, SchedulerException cause,
        CancellationToken cancellationToken = default)
    {
        logger.LogError(cause.Message, "超时" + "Quartz Scheduler Error: {Message}", msg);
        return base.SchedulerError(msg, cause, cancellationToken);
    }
}