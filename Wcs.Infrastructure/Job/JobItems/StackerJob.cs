using Common.Application.QuartzJob;
using Quartz;
using Wcs.Application.SignalR;
using Wcs.Device.Device.Stacker;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
public class StackerJob(IHubManager _hubManager, IStackerController _controller) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await _controller.ExecuteAsync(_linkedCts.Token);
        await base.Execute(context);
    }
}