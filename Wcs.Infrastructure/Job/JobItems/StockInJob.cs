using Common.Application.QuartzJob;
using Quartz;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.SignalR;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
public class StockInJob(
    IHubManager _hubManager,
    IStockPortInController _controller) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        await _controller.ExecuteAsync(_linkedCts.Token);
    }
}