using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Abstract;
using Wcs.Application.Abstract.Device;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.Tranship;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Controler;

public class StackerTranShipOutController : AbstractStackerTranshipOutController
{
    public StackerTranShipOutController(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        DeviceType = DeviceTypeEnum.StackerOutTranShip;
    }

    public override async Task ExecuteAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            await base.ExecuteAsync(token);
        }
        else
        {
            // 控制并行度（最多4个任务同时执行）     并行处理保证各个设备的处理粒度   使用CancellationToken超时处理保证业务的正常进行
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4, CancellationToken = token };
            // 使用Parallel.ForEachAsync处理异步并行          注意如果是一巷道多堆垛需要先对设备进行分组然后在进行调度
            await Parallel.ForEachAsync(Devices, parallelOptions, async (item, cancelToken) =>
            {
                //TODO 
            });
        }
    }
}