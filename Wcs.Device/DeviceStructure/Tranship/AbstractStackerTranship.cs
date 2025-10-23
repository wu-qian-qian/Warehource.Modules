using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceStructure.Tranship;

public abstract class AbstractStackerTranship : AbstractDevice<StackerTranShipConfig, PipeLineDBEntity>
{
    protected AbstractStackerTranship(string name, StackerTranShipConfig config, string regionCode, bool enable,
        string deviceGroup) :
        base(enable, regionCode)
    {
        Config = config;
        Name = name;
        Config.Name = name;
        DeviceGroupCode = deviceGroup;
    }

    public sealed override StackerTranShipConfig Config { get; protected set; }

    public override PipeLineDBEntity DBEntity { get; protected set; }


    /// <summary>
    ///     是否可以执行
    /// </summary>
    /// <returns></returns>
    public override bool CanExecute()
    {
        return DBEntity is { RMode: "1", RErrCode: "0" } && Enable;
    }

    /// <summary>
    ///     是否可以下发
    /// </summary>
    /// <returns></returns>
    public override bool IsNewStart()
    {
        return DBEntity is { RLoad: "1", RFree: "0" };
    }
}