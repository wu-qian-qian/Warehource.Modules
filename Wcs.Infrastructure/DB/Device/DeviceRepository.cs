using Common.Infrastructure.EF;
using Wcs.Domain.Device;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.Device;

public class DeviceRepository(WCSDBContext _db) : IDeviceRepository
{
    public Domain.Device.Device Get(Guid id)
    {
        return _db.Devices.FirstOrDefault(x => x.Id == id);
    }

    public Domain.Device.Device Get(string deviceName)
    {
        return _db.Devices.FirstOrDefault(x => x.DeviceName == deviceName);
    }

    public void Add(Domain.Device.Device device)
    {
        _db.Devices.Add(device);
    }

    public void Update(Domain.Device.Device device)
    {
        _db.Devices.Update(device);
    }

    public IQueryable<Domain.Device.Device> GetQueryable()
    {
        return _db.Query<Domain.Device.Device>();
    }
}