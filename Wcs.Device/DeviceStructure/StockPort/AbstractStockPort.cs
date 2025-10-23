using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceStructure.StockPort;

public abstract class AbstractStockPort : AbstractDevice<StockPortConfig, PipeLineDBEntity>
{
    protected AbstractStockPort(string name, StockPortConfig config, string regionCode, bool enable, string deviceGroup)
        : base(enable,
            regionCode)
    {
        Config = config;
        Name = name;
        Config.Name = name;
        DeviceGroupCode = deviceGroup;
    }

    public sealed override StockPortConfig Config { get; protected set; }

    public override PipeLineDBEntity DBEntity { get; protected set; }


    public override bool CanExecute()
    {
        return DBEntity is { RMode: "1", RErrCode: "0" } && Enable;
        ;
    }

    public override bool IsNewStart()
    {
        return DBEntity is { RLoad: "1", RFree: "1" };
    }
}