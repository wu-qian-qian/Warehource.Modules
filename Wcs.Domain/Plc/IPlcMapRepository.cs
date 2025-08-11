namespace Wcs.Domain.Plc;

public interface IPlcMapRepository
{
    void Insert(IEnumerable<PlcMap> plcMaps);

    void Update(IEnumerable<PlcMap> plcMaps);

    IEnumerable<PlcMap> GetPlcMapOfDeviceName(string deviceName);
}