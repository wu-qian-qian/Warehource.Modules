using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.SignalR;
using Wcs.Device.Device.Tranship;
using Wcs.Infrastructure.Device.Controler;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
public class StackerInTranShipJob(
    IHubManager _hubManager,
    [FromKeyedServices(nameof(StackerInTranShipController))]
    IStackerTranshipController _controller) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        await _controller.ExecuteAsync(_linkedCts.Token);
    }
}