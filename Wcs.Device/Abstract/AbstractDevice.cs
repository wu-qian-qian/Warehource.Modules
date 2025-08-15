using System.Linq.Expressions;
using Common.JsonExtension;

namespace Wcs.Device.Abstract;

/// <summary>
/// 设备数据结构
/// </summary>
/// <typeparam name="TConfig"></typeparam>
/// <typeparam name="TDBEntity"></typeparam>
public abstract class AbstractDevice<TConfig, TDBEntity> : IDevice<TConfig>
    where TConfig : BaseDeviceConfig where TDBEntity : BaseDBEntity
{
    public abstract TDBEntity DBEntity { get; protected set; }
    public abstract string Name { get; init; }
    public abstract TConfig Config { get; protected set; }

    /// <summary>
    ///     设置配置项
    /// </summary>
    /// <param name="config"></param>
    public virtual void SetConfig(string config)
    {
        Config = config.ParseJson<TConfig>();
    }

    /// <summary>
    /// </summary>
    /// <param name="StackerDBEntity"></param>
    public virtual void SetDBEntity(TDBEntity StackerDBEntity)
    {
        DBEntity = StackerDBEntity;
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