using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.SignalR;
using Wcs.Infrastructure.Device.Controler;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
public class StackerInTranShipJob(
    IHubManager _hubManager,
    IStackerTranshipInController _controller) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        await _controller.ExecuteAsync(_linkedCts.Token);
    }
}