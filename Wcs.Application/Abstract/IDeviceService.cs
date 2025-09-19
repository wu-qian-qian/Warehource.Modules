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

    void SetDviceEnable(bool enable, DeviceTypeEnum deviceType, string deviceName);
}