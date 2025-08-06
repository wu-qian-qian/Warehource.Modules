namespace Wcs.Domain.Device;

public interface IDeviceRepository
{
    public Device Get(Guid id);
    public Device Get(string deviceName);

    public void Add(Device device);
    public void Update(Device device);

    public IQueryable<Device> GetQueryable();
}