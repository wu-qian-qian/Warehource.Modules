using Microsoft.Extensions.DependencyInjection;
using Wcs.Device.DeviceStructure.Tranship;

namespace Wcs.Application.DeviceController.Tranship;

public abstract class AbstractStackerTranshipController : BaseDeviceController<AbstractStackerTranship>,
    IStackerTranshipController
{
    protected AbstractStackerTranshipController(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
    }

    public virtual async Task ExecuteAsync(CancellationToken token = default)
    {
        await InitializeAsync();
    }


    /// <summary>
    ///     TODO 根据(变量)当前的状态来获得可以执行的借货后
    /// </summary>
    /// <returns></returns>
    public virtual string[] GetReCommendTranship(string region)
    {
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