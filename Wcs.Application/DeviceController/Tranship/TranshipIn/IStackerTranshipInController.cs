namespace Wcs.Application.DeviceController.Tranship;

public interface IStackerTranshipInController : ITranshipController, ICommonController
{
    /// <summary>
    ///     根据区域编码获取该区域下的所有隧道和输送编码
    /// </summary>
    /// <param name="regionCode"></param>
    /// <returns></returns>
    public IEnumerable<KeyValuePair<string, string>> GetTunnleAndPiplineCodeByRegion(string regionCode);

    /// <summary>
    ///     根据设备名称获取任务编号
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public string GetWcsTaskNoByDeviceName(string deviceName);

    /// <summary>
    ///     根据设备名称获取位置编码
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    string GetLocationByDeviceName(string deviceName);

    /// <summary>
    ///     根据设备名称获取隧道编码
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public string GetTunnleByDeviceName(string deviceName);

    /// <summary>
    ///     根据隧道编码获取任务编号
    /// </summary>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    public string GetWcsTaskNoByTunnle(string tunnle);
}