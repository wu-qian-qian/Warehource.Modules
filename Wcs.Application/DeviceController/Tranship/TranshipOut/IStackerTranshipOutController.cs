using Wcs.CustomEvents.Saga;

namespace Wcs.Application.DeviceController.Tranship.TranshipOut;

public interface IStackerTranshipOutController : ITranshipController, ICommonController
{
    /// <summary>
    ///     通过巷道获取位置编码
    /// </summary>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    string GetLocationByTunnle(string tunnle);

    /// <summary>
    ///     通过巷道获取设备名称
    /// </summary>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    string GetDeviceNameByTunnle(string tunnle);

    string GetPiplineByDeviceName(string deviceName);

    /// <summary>
    ///     根据设备名称获取写PLC任务数据
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    WcsWritePlcTaskCreated TryGetWritePlcTaskDataByDeviceName(string deviceName);
}