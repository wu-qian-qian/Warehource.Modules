using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Abstract.Device;
using Wcs.Application.Handler.DeviceExecute;
using Wcs.Application.Handler.DeviceExecute.StackerTranshipIn;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Controler;

internal class StackerInTranShipController : AbstractStackerTranshipInController
{
    public StackerInTranShipController(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        DeviceType = DeviceTypeEnum.StackerInTranShip;
    }

    public override async Task ExecuteAsync(CancellationToken token = default)
    {
        if (Devices != null && Devices.Any())
        {
            // 控制并行度（最多4个任务同时执行）     并行处理保证各个设备的处理粒度   使用CancellationToken超时处理保证业务的正常进行
            var parallelOptions = new ParallelOptions
                { MaxDegreeOfParallelism = Devices.Length, CancellationToken = token };
            // 使用Parallel.ForEachAsync处理异步并行          注意如果是一巷道多堆垛需要先对设备进行分组然后在进行调度
            await Parallel.ForEachAsync(Devices, parallelOptions, async (item, cancelToken) =>
            {
                //一条线程一个执行周期
                using var scope = _scopeFactory.CreateScope();
                var sender = scope.ServiceProvider.GetService<ISender>();
                IExecuteDeviceCommand request = new StackerTranshipInCommand(item);
                await sender.Send(request, cancelToken);
            });
        }
        else
        {
            await base.ExecuteAsync(token);
        }
    }
}