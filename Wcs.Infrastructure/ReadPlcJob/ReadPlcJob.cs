using Common.Application.Event;
using Common.Application.QuartzJob;
using MediatR;
using Quartz;
using Wcs.Application.Custom;
using Wcs.Application.S7Plc.ReadPlcCommand;

namespace Wcs.Infrastructure.ReadPlcJob;

[DisallowConcurrentExecution]
internal class ReadPlcJob(IMassTransitEventBus bus, ISender sender) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        Console.WriteLine("启动");
        try
        {
            await bus.PublishAsync(new SendWmsTasksIntegrationEvent(DateTime.Now));
        }
        catch (OperationCanceledException ex)
        {
            throw new JobExecutionException($"任务执行超时，已取消: {ex.Message}", ex, true);
        }

        Console.WriteLine("结束");
    }

    private async Task ReadPlc()
    {
        sender.Send(new PlcEventCommand());
    }
}