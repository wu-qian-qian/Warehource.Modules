using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Execute.Business.Stacker;
using Wcs.Application.Handler.Execute.CreatDeviceData;
using Wcs.Device.Device.Stacker;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Controler;

/// <summary>
///     堆垛机调度中心
///     只是用来处理一些调度
///     单例注入
/// </summary>
internal class StackerController : BaseDependy, IStackerController
{
    public StackerController(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
        DeviceType = DeviceTypeEnum.Stacker;
    }


    public AbstractStacker[] Devices { get; private set; }

    public DeviceTypeEnum DeviceType { get; init; }

    public async Task ExecuteAsync(CancellationToken token = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var sender = scope.ServiceProvider.GetService<ISender>();
        if (Devices == null || Devices.Length == 0)
        {
            Devices = (AbstractStacker[])await sender.Send(new CreatDeviceDataCommand
            {
                DeviceType = this.DeviceType,
            });
        }
        else
        {
            // 控制并行度（最多4个任务同时执行）     并行处理保证各个设备的处理粒度   使用CancellationToken超时处理保证业务的正常进行
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4, CancellationToken = token };
            // 使用Parallel.ForEachAsync处理异步并行          注意如果是一巷道多堆垛需要先对设备进行分组然后在进行调度
            await Parallel.ForEachAsync(Devices, parallelOptions, async (item, cancelToken) =>
            {
                await sender.Send(new StackerCommand
                {
                    Stacker = item
                }, cancelToken);
            });
        }
    }
}