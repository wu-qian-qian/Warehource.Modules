namespace Wcs.Device.Abstract;

public interface IDevice<T> where T : class
{
    string Name { get; }

    T Config { get; }
}