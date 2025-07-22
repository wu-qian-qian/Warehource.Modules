using Common.Application.QuartzJob;
using MediatR;
using Quartz;
using Wcs.Application.Abstract;

namespace Wcs.Infrastructure.ReadPlcJob;


/// <summary>
/// 用来重连
/// </summary>
/// <param name="sender"></param>
/// <param name="netService"></param>
[DisallowConcurrentExecution]
public class ConnectPlcJob(ISender sender, INetService netService) : BaseJob
{
    public override Task Execute(IJobExecutionContext context)
    {
        return base.Execute(context);
    }
}