using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.Tranship;
using Wcs.Shared;

namespace Wcs.Application.Abstract.Device;

public abstract class AbstractStackerTranshipController : IStackerTranshipController
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected AbstractStackerTranshipController(IServiceScopeFactory serviceScopeFactory)
    {
    }

    public AbstractStackerTranship[] Devices { get; private set; }

    public DeviceTypeEnum DeviceType { get; init; }

    public virtual async Task ExecuteAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetService<ISender>();
            Devices = (AbstractStackerTranship[])await sender.Send(new CreatDeviceDataCommand
            {
                DeviceType = DeviceType
            });
        }
    }

    public void SetEnable(bool enable, string name)
    {
        if (Devices.Any(d => d.Name == name)) Devices.First().SetEnable(enable);
    }

    /// <summary>
    ///     TODO 根据(变量)当前的状态来获得可以执行的借货后
    /// </summary>
    /// <returns></returns>
    public virtual string[] GetReCommendTranship(string region)
    {
        var tunnle = 0;
        var tempDevices = Devices.Where(p => p.Enable && p.CanRegionExecute(region));
        return tempDevices.Select(p => p.Config.Tunnle).ToArray();
    }

    public string GetCurrentPipline(string name)
    {
        var pipline = string.Empty;
        if (Devices.Any(d => d.Name == name && d.Enable)) pipline = Devices.First().Config.PipelinCode;
        return pipline;
    }

    /// <summary>
    ///     巷道获取当前位置
    /// </summary>
    /// <param name="tunnle"></param>
    /// <param name="region"></param>
    /// <returns></returns>
    public string GetCurrentPosWithTunnle(string tunnle, string region)
    {
        var pos = string.Empty;
        var config = Devices
            .FirstOrDefault(d => d.CanRegionExecute(region) && d.Enable)?.Config;
        if (config != null) pos = $"{config.Tunnle}_{config.Row}_{config.Column}_{config.Floor}__{config.Depth}";
        return pos;
    }

    public string GetDeviceNameWithTunnle(string tunnle, string region)
    {
        var deviceName = string.Empty;
        var device = Devices
            .FirstOrDefault(d => d.CanRegionExecute(region) && d.Enable);
        if (device != null) deviceName = device.Name;
        return deviceName;
    }
}