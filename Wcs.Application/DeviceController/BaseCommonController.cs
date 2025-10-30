using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Abstract;
using Wcs.Device.DeviceStructure.StockPort;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.DeviceController;

/// <summary>
///     控制中心的公共操作抽象
///     对ICommonController的一些公共接口
/// </summary>
/// <typeparam name="TDeviceStructure"></typeparam>
public abstract class BaseCommonController<TDeviceStructure> where TDeviceStructure : class, IDevice
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected BaseCommonController(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public TDeviceStructure[] Devices { get; protected set; }

    public DeviceTypeEnum DeviceType { get; init; }

    /// <summary>
    /// 用来初始化当前设备类型控制中心所有拥有的设备数据结构
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    protected async ValueTask InitializeAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetService<ISender>();
            var res = await sender.Send(new CreatDeviceDataCommand
            {
                DeviceType = DeviceType
            });
            //安全转换
            Devices = res.OfType<TDeviceStructure>().ToArray();
        }
    }

    #region 公共基本的业务

    /// <summary>
    ///设置启用禁用
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="name"></param>
    public void SetEnable(bool enable, string name)
    {
        Devices.First().SetEnable(enable);
    }

    /// <summary>
    /// 通过设备名称获取当前设备任务
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public WcsTask? GetWcsTaskByDeviceName(string deviceName)
    {
        return Devices.First(d => d.Name == deviceName).WcsTask;
    }

    /// <summary>
    /// 通过设备名称设置当前设备任务
    /// </summary>
    /// <param name="deviceName"></param>
    /// <param name="wcsTask"></param>
    public void SetWcsTask(string deviceName, WcsTask wcsTask)
    {
        var device = Devices.First(d => d.Name == deviceName);
        device.SetWcsTask(wcsTask);
    }

    /// <summary>
    /// 通过设备名称获取当前设备所管辖的区域编码
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public string GetRegionCodesByDeviceName(string deviceName)
    {
        var device = Devices.First(d => d.Name == deviceName);
        return device.RegionCodes;
    }

    /// <summary>
    /// 通过设备名称获取当前设备数据结构
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public TDeviceStructure GetDevice(string deviceName)
    {
        return Devices.First();
    }

    /// <summary>
    /// 通过设备名称清除当前设备任务
    /// </summary>
    /// <param name="deviceName"></param>
    public void CleatWcsTaskByDeviceName(string deviceName)
    {
        Devices.First(d => d.Name == deviceName).ClearWcsTask();
    }

    /// <summary>
    /// 通过设备名称获取当前设备任务缓存key
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public string GetWcsTaskCacheOfKey(string deviceName)
    {
        return Devices.First(d => d.Name == deviceName).GetCacheTaskKey();
    }

    #endregion
}