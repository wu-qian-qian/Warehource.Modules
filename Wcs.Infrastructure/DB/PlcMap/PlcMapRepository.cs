using Common.Infrastructure.EF;
using Wcs.Domain.Plc;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.PlcMap;

internal class PlcMapRepository(WCSDBContext _db) : IPlcMapRepository
{
    public IEnumerable<Domain.Plc.PlcMap> GetPlcMapOfDeviceName(string deviceName)
    {
        return _db.Query<Domain.Plc.PlcMap>().Where(p => p.DeviceName == deviceName).ToArray();
    }

    public void Insert(IEnumerable<Domain.Plc.PlcMap> plcMaps)
    {
        _db.PlcMaps.AddRange(plcMaps);
    }

    public void Update(IEnumerable<Domain.Plc.PlcMap> plcMaps)
    {
        _db.PlcMaps.UpdateRange(plcMaps);
    }
}