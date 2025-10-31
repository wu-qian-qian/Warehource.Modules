using System.Linq.Expressions;
using Common.JsonExtension;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Device.Abstract;

/// <summary>
///     设备数据结构
/// </summary>
/// <typeparam name="TConfig"></typeparam>
/// <typeparam name="TDBEntity"></typeparam>
public abstract class AbstractDevice<TConfig, TDBEntity> : IDevice<TConfig>
    where TConfig : BaseDeviceConfig where TDBEntity : BaseDBEntity, new()
{
    protected AbstractDevice(bool enable, string regionCodes)
    {
        Enable = enable;
        RegionCodes = regionCodes;
        DBEntity = new TDBEntity();
    }

    /// <summary>
    ///     信号量
    /// </summary>
    public abstract TDBEntity DBEntity { get; protected set; }

    /// <summary>
    ///     是否启动
    /// </summary>
    public bool Enable { get; protected set; }

    /// <summary>
    ///     设备组
    /// </summary>
    public string DeviceGroupCode { get; protected set; }

    /// <summary>
    ///     设备类型
    /// </summary>
    public DeviceTypeEnum DeviceType { get; init; }

    /// <summary>
    ///     设备的区域编码组
    ///     可能包含多个
    ///     因为一个区域表示一条路径，且多调路径可能经过某同一设备
    /// </summary>
    public string RegionCodes { get; protected set; }

    /// <summary>
    ///     设备名
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     设备的独立配置文件
    /// </summary>
    public abstract TConfig Config { get; protected set; }


    public WcsTask? WcsTask { get; protected set; }


    /// <summary>
    ///     是否可以执行新任务
    /// </summary>
    /// <returns></returns>
    public abstract bool IsNewStart();

    /// <summary>
    ///     是否可以执行
    /// </summary>
    /// <returns></returns>
    public abstract bool CanExecute();


    /// <summary>
    ///     设置配置项
    /// </summary>
    /// <param name="config"></param>
    public virtual void SetConfig(string config)
    {
        Config = config.ParseJson<TConfig>();
    }

    /// <summary>
    ///     创建出键值对
    /// </summary>
    /// <param name="propertyExpression"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public string CreatWriteExpression(Expression<Func<TDBEntity, string>> propertyExpression)
    {
        var key = GetPropertyName(propertyExpression);
        return key;
    }

    private static string GetPropertyName<T>(Expression<Func<T, string>> propertyExpression)
    {
        // 解析表达式，获取属性访问节点
        MemberExpression memberExpr;

        // 处理值类型的装箱情况（如果属性是值类型，表达式会被包装为Convert）
        if (propertyExpression.Body is UnaryExpression unaryExpr &&
            unaryExpr.Operand is MemberExpression)
            memberExpr = (MemberExpression)unaryExpr.Operand;
        // 直接是属性访问表达式
        else if (propertyExpression.Body is MemberExpression)
            memberExpr = (MemberExpression)propertyExpression.Body;
        else
            throw new ArgumentException("表达式必须是属性访问表达式", nameof(propertyExpression));

        // 返回属性名称
        return memberExpr.Member.Name;
    }


    #region 抽象的实现

    /// <summary>
    ///     设置任务
    /// </summary>
    /// <param name="wcsTask"></param>
    public void SetWcsTask(WcsTask wcsTask)
    {
        if (WcsTask != null) WcsTask = wcsTask;
    }

    public string GetCacheTaskKey()
    {
        return Config.TaskKey;
    }

    public string GetCacheDBKey()
    {
        return Config.DBKey;
    }

    public string GetCacheKey()
    {
        return Config.Key;
    }

    /// <summary>
    ///     清除任务
    /// </summary>
    public void ClearWcsTask()
    {
        WcsTask = default;
    }

    public virtual void SetEnable(bool enable)
    {
        if (Enable != enable) Enable = enable;
    }

    /// <summary>
    ///     设备是否可以执行该区域
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    public bool CanRegionExecute(string region)
    {
        if (string.IsNullOrEmpty(RegionCodes)) return true;
        if (region.Contains(RegionCodes) || RegionCodes.Contains(region)) return true;
        return false;
    }

    /// <summary>
    ///     设置DB实体
    /// </summary>
    /// <param name="dBEntity"></param>
    public void SetDBEntiry(BaseDBEntity dBEntity)
    {
        DBEntity = (TDBEntity)dBEntity;
    }

    #endregion
}