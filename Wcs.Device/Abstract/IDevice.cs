namespace Wcs.Device.Abstract;

public interface IDevice<T> where T : BaseDeviceConfig
{
    string Name { get; }

    T Config { get; }
}