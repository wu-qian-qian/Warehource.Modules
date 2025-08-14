using Common.JsonExtension;
using Wcs.Device.Abstract;
using Wcs.Device.Config;
using Wcs.Device.DeviceDB;

namespace Wcs.Device.BaseDevice;

/// <summary>
///     TODO 变量业务逻辑判断
/// </summary>
public abstract class AbstractStacker : AbstractDevice<StackerConfig, StackerDBEntity>
{
    public override string Name { get; init; }

    public override StackerConfig Config { get; protected set; }

    public override StackerDBEntity DBEntity { get; protected set; }


    /// <summary>
    ///     设置配置项
    /// </summary>
    /// <param name="config"></param>
    public override void SetConfig(string config)
    {
        Config = config.ParseJson<StackerConfig>();
    }

    /// <summary>
    /// </summary>
    /// <param name="StackerDBEntity"></param>
    public override void SetDBEntity(StackerDBEntity StackerDBEntity)
    {
        DBEntity = StackerDBEntity;
    }

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