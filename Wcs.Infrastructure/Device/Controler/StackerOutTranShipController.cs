using Common.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.DeviceController;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Application.DeviceController.Tranship.TranshipOut;
using Wcs.Application.Handler.DeviceExecute;
using Wcs.Application.Handler.DeviceExecute.StackerTranshipOut;
using Wcs.CustomEvents.Saga;
using Wcs.Device.DeviceStructure.Tranship;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Controler;

[DependyAttrubite(DependyLifeTimeEnum.Singleton, typeof(IStackerTranshipOutController))]
public class StackerOutTranShipController : BaseCommonController<AbstractStackerTranship>,
    IStackerTranshipOutController
{
    public StackerOutTranShipController(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        DeviceType = DeviceTypeEnum.StackerOutTranShip;
    }

    public async ValueTask ExecuteAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            await InitializeAsync();
        }
        else
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
                IExecuteDeviceCommand request = new StackerTranshipOutCommand(item);
                await sender.Send(request, cancelToken);
            });
        }
    }


    #region

    public string GetLocationByTunnle(string tunnle)
    {
        var device = Devices.First(p => p.Config.Tunnle == tunnle);
        //如果一个巷道多个堆垛机还需要加上区域区分 
        return $"{device.Config.Tunnle}_{device.Config.Floor}_{device.Config.Row}_{device.Config.Column}_1";
    }

    public string GetDeviceNameByTunnle(string tunnle)
    {
        return Devices.First(p => p.Config.Tunnle == tunnle).Name;
    }

    public string GetPiplineByDeviceName(string deviceName)
    {
        return Devices.First(p => p.Name == deviceName).Config.PipelinCode;
    }

    public WcsWritePlcTaskCreated TryGetWritePlcTaskDataByDeviceName(string deviceName)
    {
        var device = Devices.First(d => d.Name == deviceName);
        var wcsTask = device.WcsTask;
        var dic = new Dictionary<string, string>();
        dic.Add(device.CreatWriteExpression(p => p.WTask), wcsTask.SerialNumber.ToString());
        dic.Add(device.CreatWriteExpression(p => p.WTargetCode), wcsTask.StartPosition);
        dic.Add(device.CreatWriteExpression(p => p.WTaskType), ((int)wcsTask.TaskType).ToString());
        dic.Add(device.CreatWriteExpression(p => p.WStart), "1");
        return new WcsWritePlcTaskCreated(deviceName, dic,
            device.Config.TaskKey);
    }

    #endregion
}