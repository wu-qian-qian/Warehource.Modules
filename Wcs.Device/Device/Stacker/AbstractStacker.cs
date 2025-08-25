using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.Device.Stacker;

/// <summary>
///     TODO 变量业务逻辑判断
///     设备的数据结构
/// </summary>
public abstract class AbstractStacker : AbstractDevice<StackerConfig, StackerDBEntity>
{
    protected AbstractStacker(string name, StackerConfig config, string regionCode, bool enable) : base(enable,
        regionCode)
    {
        Name = name;
        Config = config;
    }

    public override StackerConfig Config { get; protected set; }

    public override StackerDBEntity DBEntity { get; protected set; }


    /// <summary>
    ///     是否可以写入变量状态
    /// </summary>
    /// <returns></returns>
    public override bool IsNewStart()
    {
        return true;
    }

    /// <summary>
    ///     是否可以执行
    /// </summary>
    /// <returns></returns>
    public override bool CanExecute()
    {
        return true;
    }

    /// <summary>
    ///     是否堆垛机接货点位
    /// </summary>
    /// <returns></returns>
    public bool IsTranShipPoint()
    {
        //TODO 
        return true;
    }

    /// <summary>
    ///     是否完成
    /// </summary>
    /// <returns></returns>
    public bool IsComplate()
    {
        return true;
    }
}