using Wcs.Application.DeviceController.StockPort;
using Wcs.CustomEvents.Saga;

namespace Wcs.Application.DeviceController.Tranship;

/// <summary>
///     货物入库口业务逻辑处理接口
/// </summary>
public interface IStockPortInController : ICommonController, IStockPortController
{
    /// <summary>
    ///     尝试获取写PLC任务数据
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    WcsWritePlcTaskCreated TryGetWritePlcTaskDataByDeviceName(string deviceName);

    /// <summary>
    ///     获取条码信息
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    string GetBarCodeByDeviceName(string deviceName);

    /// <summary>
    ///     获取设备编号
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    string GetPiplineByDeviceName(string deviceName);
}