using Wcs.Application.DeviceController.StockPort;

namespace Wcs.Application.DeviceController.Tranship;

public interface IStockPortOutController : ICommonController, IStockPortController
{
    /// <summary>
    ///     获取设备任务
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    string GetWcsTaskCodeByDeviceName(string deviceName);
}