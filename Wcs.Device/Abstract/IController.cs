using Wcs.Shared;

namespace Wcs.Device.Abstract;

public interface IController<T> : IController where T : class
{
    T[] Devices { get; }
}

public interface IController
{
    DeviceTypeEnum DeviceType { get; }
    Task ExecuteAsync(CancellationToken token = default);
}