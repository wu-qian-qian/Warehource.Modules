using Wcs.Contracts.Respon.Device;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceStructure.StockPort;

public class StockInPort : AbstractStockPort
{
    public StockInPort(StockPortConfig config, DeviceDto device) : base(device.DeviceName, config, device.RegionCode,
        device.Enable, device.GroupName)
    {
        DeviceType = DeviceTypeEnum.StockPortIn;
    }

    public override bool IsNewStart()
    {
        //TODO 变量判断是否可以下发任务
        return true;
    }
}