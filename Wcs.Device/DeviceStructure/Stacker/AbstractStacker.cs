using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceStructure.Stacker;

/// <summary>
///     TODO 变量业务逻辑判断
///     设备的数据结构
/// </summary>
public abstract class AbstractStacker : AbstractDevice<StackerConfig, StackerDBEntity>
{
    protected AbstractStacker(string name, StackerConfig config, string regionCode, bool enable, string deviceGroup) :
        base(enable,
            regionCode)
    {
        Name = name;
        Config = config;
        Config.Name = name;
        DeviceGroupCode = deviceGroup;
    }

    public sealed override StackerConfig Config { get; protected set; }

    public override StackerDBEntity DBEntity { get; protected set; }


    /// <summary>
    ///     是否可以写入变量状态
    /// </summary>
    /// <returns></returns>
    public override bool IsNewStart()
    {
        return DBEntity is { RLoad: "0", RExecuteStatus: "1" } && Enable;
    }

    /// <summary>
    ///     是否可以执行
    ///     在线无报警表示可以行走
    /// </summary>
    /// <returns></returns>
    public override bool CanExecute()
    {
        return DBEntity is { RMode: "2", RErrCode: "0" } && Enable;
    }

    /// <summary>
    ///     是否堆垛机接货点位
    ///     入库工位，
    /// </summary>
    /// <returns></returns>
    public bool IsTranShipPoint()
    {
        if (DBEntity.RRow == Config.StationRow && DBEntity.RColumn == Config.StationColumn) return true;

        return false;
    }

    /// <summary>
    ///     是否完成
    /// </summary>
    /// <returns></returns>
    public bool IsComplate()
    {
        return DBEntity.RExecuteStatus == "2";
    }
}