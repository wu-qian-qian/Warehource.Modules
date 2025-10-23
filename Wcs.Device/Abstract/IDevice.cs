namespace Wcs.Device.Abstract;

public interface IDevice<T> : IDevice where T : BaseDeviceConfig
{
    T Config { get; }
}

public interface IDevice
{
    string Name { get; }
    public void SetEnable(bool enable);
}