using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.Device.Tranship;

public abstract class AbstractStackerTranship : AbstractDevice<StackerTranShipConfig, PipeLineDBEntity>
{
    protected AbstractStackerTranship(StackerTranShipConfig config, string name, string regionCodes, bool enable) :
        base(enable, regionCodes)
    {
        Config = config;
        Name = name;
    }

    public override StackerTranShipConfig Config { get; protected set; }

    public override PipeLineDBEntity DBEntity { get; protected set; }


    public override bool CanExecute()
    {
        throw new NotImplementedException();
    }

    public override bool IsNewStart()
    {
        throw new NotImplementedException();
    }
}