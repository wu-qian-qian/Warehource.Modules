using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.SignalR;
using Wcs.Device.Device.StockPort;
using Wcs.Infrastructure.Device.Controler;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
public class StockOutJob(
    IHubManager _hubManager,
    [FromKeyedServices(nameof(StockPortOutController))]
    IStockPortController _controller) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await _controller.ExecuteAsync(_linkedCts.Token);
        await base.Execute(context);
    }
}