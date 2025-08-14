using System.Linq.Expressions;
using Wcs.Application.Abstract.Device;
using Wcs.Device.DeviceDB;

namespace Wcs.Application.Abstract;

public abstract class AbstractDevice<TConfig, TDBEntity> : IDevice<TConfig>
    where TConfig : class where TDBEntity : BaseEntity
{
    public abstract TDBEntity DBEntity { get; protected set; }
    public abstract string Name { get; init; }
    public abstract TConfig Config { get; protected set; }

    public abstract void SetConfig(string config);

    public abstract void SetDBEntity(TDBEntity config);

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