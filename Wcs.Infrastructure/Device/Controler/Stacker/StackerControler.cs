using Common.JsonExtension;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.DBHandler.Device.Get;
using Wcs.Application.DeviceHandler.Business.Stacker;
using Wcs.Device.Config;
using Wcs.Device.Device.BaseExecute;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Controler.Stacker;

/// <summary>
///     堆垛机调度中心
///     只是用来处理一些调度
///     单例注入
/// </summary>
internal class StackerControler : IStackerControler
{
    private readonly IServiceScopeFactory _scopeFactory;

    public StackerControler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        DeviceType = DeviceTypeEnum.Stacker;
        Stackers = new Execute.Stacker.Stacker[] { };
    }

    public DeviceTypeEnum DeviceType { get; init; }

    private AbstractStacker[] Stackers { get; set; }

    public async Task ExecuteAsync(CancellationToken token = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var sender = scope.ServiceProvider.GetService<ISender>();
        if (Stackers.Any() == false)
        {
            var result = await sender.Send(new GetDeviceQuery
            {
                DeviceType = DeviceType,
                Enable = true
            }, token);
            if (result.IsSuccess)
            {
                var devices = result.Value.ToArray();
                var tempStacker = new List<AbstractStacker>();
                for (var i = 0; i < devices.Length; i++)
                    if (Stackers.Any(p => p.Name == devices[i].DeviceName) == false)
                    {
                        var config = devices[i].Config.ParseJson<StackerConfig>();
                        AbstractStacker stacker = new Execute.Stacker.Stacker(devices[i].DeviceName, config);
                        tempStacker.Add(stacker);
                    }

                if (tempStacker.Any()) Stackers = tempStacker.ToArray();
            }
        }

        // 控制并行度（最多4个任务同时执行）     并行处理保证各个设备的处理粒度   使用CancellationToken超时处理保证业务的正常进行
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4, CancellationToken = token };
        // 使用Parallel.ForEachAsync处理异步并行          注意如果是一巷道多堆垛需要先对设备进行分组然后在进行调度
        await Parallel.ForEachAsync(Stackers, parallelOptions, async (item, cancelToken) =>
        {
            await sender.Send(new StackerCommandEvent
            {
                Stacker = item
            }, cancelToken);
        });
    }
}