using Common.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.DeviceController;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.Handler.DeviceExecute;
using Wcs.Application.Handler.DeviceExecute.StackerTranshipIn;
using Wcs.Device.DeviceStructure.Tranship;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Controler;

[DependyAttrubite(DependyLifeTimeEnum.Singleton, typeof(IStackerTranshipInController))]
internal class StackerInTranShipController : BaseCommonController<AbstractStackerTranship>,
    IStackerTranshipInController
{
    public StackerInTranShipController(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        DeviceType = DeviceTypeEnum.StackerInTranShip;
    }


    public async ValueTask ExecuteAsync(CancellationToken token = default)
    {
        if (Devices != null && Devices.Any())
        {
            // 控制并行度（最多4个任务同时执行）     并行处理保证各个设备的处理粒度   使用CancellationToken超时处理保证业务的正常进行
            var parallelOptions = new ParallelOptions
                { MaxDegreeOfParallelism = 1, CancellationToken = token };
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
            await InitializeAsync();
        }
    }

    #region bussiness methd

    public IEnumerable<KeyValuePair<string, string>> GetTunnleAndPiplineCodeByRegion(string regionCode)
    {
        var tunnles = Devices.Select(device =>
        {
            if (device.CanExecute() && device.CanRegionExecute(regionCode)) return device.Config.Tunnle;

            return string.Empty;
        });
        tunnles = tunnles.Where(t => t != string.Empty).Distinct().ToArray()!;
        foreach (var tunnle in tunnles)
        {
            var target = Devices.First(d => d.Config.Tunnle == tunnle).Config.PipelinCode;
            yield return new KeyValuePair<string, string>(tunnle, target);
        }
    }

    public string GetWcsTaskNoByDeviceName(string deviceName)
    {
        var device = Devices.First(p => p.Name == deviceName);
        if (device.CanExecute()) return device.DBEntity.RTask;

        return string.Empty;
    }

    public string GetLocationByDeviceName(string deviceName)
    {
        var device = Devices.First(p => p.Name == deviceName);
        return $"{device.Config.Tunnle}_{device.Config.Floor}_{device.Config.Row}_{device.Config.Column}_1";
    }

    public string GetTunnleByDeviceName(string deviceName)
    {
        return Devices.First(p => p.Name == deviceName).Config.Tunnle;
    }

    public string GetWcsTaskNoByTunnle(string tunnle)
    {
        var devices = Devices.Where(p => p.Config.Tunnle == tunnle);
        foreach (var device in devices)
            if (device.CanExecute())
                return device.DBEntity.RTask;

        return string.Empty;
    }

    #endregion
}