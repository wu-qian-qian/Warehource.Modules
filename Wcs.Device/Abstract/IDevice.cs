using Wcs.Domain.Task;

namespace Wcs.Device.Abstract;

public interface IDevice<T> : IDevice where T : BaseDeviceConfig
{
    T Config { get; }
}

public interface IDevice
{
    string Name { get; }

    string RegionCodes { get; }
    WcsTask? WcsTask { get; }

    /// <summary>
    /// 清除当前设备任务
    /// </summary>
    void ClearWcsTask();

    /// <summary>
    /// 设置当前设备任务
    /// </summary>
    /// <param name="wcsTask"></param>
    void SetWcsTask(WcsTask wcsTask);

    /// <summary>
    /// 设置启用禁用当前设备
    /// </summary>
    /// <param name="enable"></param>
    void SetEnable(bool enable);

    /// <summary>
    /// 获取任务数据的缓存信息
    /// </summary>
    /// <returns></returns>
    string GetCacheTaskKey();

    /// <summary>
    /// 获取db数据的缓存
    /// </summary>
    /// <returns></returns>
    string GetCacheDBKey();

    /// <summary>
    /// 获取缓存key
    /// </summary>
    /// <returns></returns>
    string GetCacheKey();

    /// <summary>
    /// 当前区域是否可以执行
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    bool CanRegionExecute(string region);
}