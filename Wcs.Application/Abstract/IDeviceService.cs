using Wcs.Shared;

namespace Wcs.Application.Abstract;

public interface IDeviceService
{
    /// <summary>
    ///     获取推荐巷道
    /// </summary>
    /// <returns></returns>
    Task<string[]?> GetCanExecuteTunnleAsync(DeviceTypeEnum deviceType);

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
    Task<string> GetTranshipPositionAsync(DeviceTypeEnum deviceType, string tunnle);

    /// <summary>
    ///     获取设备名
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    Task<string> GetDeviceNameAsync(DeviceTypeEnum deviceType, string title);
}