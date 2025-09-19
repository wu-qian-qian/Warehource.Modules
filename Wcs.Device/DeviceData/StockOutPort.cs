using Wcs.Contracts.Respon.Device;
using Wcs.Device.Device.StockPort;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceData;

public class StockOutPort : AbstractStockPort
{
    public StockOutPort(StockPortConfig config, DeviceDto device) : base(device.DeviceName, config, device.RegionCode,
        device.Enable, device.GroupName)
    {
        DeviceType = DeviceTypeEnum.StockPortOut;
    }

    public override bool IsNewStart()
    {
        throw new NotImplementedException();
    }
}