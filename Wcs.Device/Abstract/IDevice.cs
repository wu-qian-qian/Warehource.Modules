namespace Wcs.Device.Device;

public interface IDevice<T> where T : class
{
    string Name { get; }

    T Config { get; }
}