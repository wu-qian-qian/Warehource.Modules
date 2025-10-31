using Wcs.Shared;

namespace Wcs.Device.Abstract;

public interface IController<T> : IController where T : class, IDevice
{
    /// <summary>
    ///     设备结构
    /// </summary>
    T[] Devices { get; }

    T GetDevice(string name);
}

public interface IController
{
    /// <summary>
    ///     设备类型
    /// </summary>
    DeviceTypeEnum DeviceType { get; }

    /// <summary>
    ///     逻辑执行
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask ExecuteAsync(CancellationToken token = default);
}