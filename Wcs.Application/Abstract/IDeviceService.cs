using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Abstract;

public interface IDeviceService
{
    /// <summary>
    ///     获取推荐巷道
    /// </summary>
    /// <returns></returns>
    Task<string[]?> GetCanExecuteTunnleAsync(DeviceTypeEnum deviceType, string regionCode);

    /// <summary>
    ///     /
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    Task<string> GetTargetPipelinAsync(DeviceTypeEnum deviceType, string deviceName);

    /// <summary>
    ///     获取取放货口坐标
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    Task<string> GetTranshipPositionAsync(DeviceTypeEnum deviceType, string tunnle, string region);

    /// <summary>
    ///     获取设备名
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    Task<string> GetDeviceNameWithTunnleAsync(DeviceTypeEnum deviceType, string tunnle, string region);

    /// <summary>
    ///     获取设备名
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    Task<string> GetDeviceNameWithTargetCodeAsync(DeviceTypeEnum deviceType, string pipLineCode, string region);

    /// <summary>
    ///     ///     设置设备启用状态
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="deviceType"></param>
    /// <param name="deviceName"></param>
    void SetDviceEnable(bool enable, DeviceTypeEnum deviceType, string deviceName);

    /// <summary>
    ///     ///     根据设备名称获取任务
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    WcsTask GetWcsTaskByDeviceNameAsync(string deviceName);
}