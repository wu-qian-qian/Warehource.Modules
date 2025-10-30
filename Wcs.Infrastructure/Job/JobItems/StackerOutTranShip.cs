using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.DeviceController.Tranship.TranshipOut;
using Wcs.Application.SignalR;
using Wcs.Infrastructure.Device.Controler;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
public class StackerOutTranShip(
    IHubManager _hubManager,
    IStackerTranshipOutController _controller) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        await _controller.ExecuteAsync(_linkedCts.Token);
    }
}