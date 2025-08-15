namespace Wcs.Device.Abstract;

public interface IDevice<T> : IDevice where T : BaseDeviceConfig
{
    string Name { get; }

    T Config { get; }
}

public interface IDevice
{
}