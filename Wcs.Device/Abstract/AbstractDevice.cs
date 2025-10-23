using System.Linq.Expressions;
using Common.JsonExtension;
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
    ///     设备类型
    /// </summary>
    public DeviceTypeEnum DeviceType { get; init; }

    /// <summary>
    ///     信号量
    /// </summary>
    public abstract TDBEntity DBEntity { get; protected set; }

    /// <summary>
    ///     是否启动
    /// </summary>
    public bool Enable { get; protected set; }

    /// <summary>
    ///     设备的区域编码组
    ///     可能包含多个
    ///     因为一个区域表示一条路径，且多调路径可能经过某同一设备
    /// </summary>
    public string RegionCodes { get; protected set; }

    /// <summary>
    ///     设备组
    /// </summary>
    public string DeviceGroupCode { get; protected set; }

    /// <summary>
    ///     设备名
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     设备的独立配置文件
    /// </summary>
    public abstract TConfig Config { get; protected set; }

    public virtual void SetEnable(bool enable)
    {
        if (Enable != enable) Enable = enable;
    }

    /// <summary>
    ///     设置配置项
    /// </summary>
    /// <param name="config"></param>
    public virtual void SetConfig(string config)
    {
        Config = config.ParseJson<TConfig>();
    }

    /// <summary>
    ///     设备是否可以执行该区域
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    public virtual bool CanRegionExecute(string region)
    {
        if (region.Contains(RegionCodes) || RegionCodes.Contains(region)) return true;
        return false;
    }

    public abstract bool IsNewStart();

    public abstract bool CanExecute();

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
}