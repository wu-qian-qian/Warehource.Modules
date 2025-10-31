namespace Common.Shared;

/// <summary>
///     依赖注入特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
public class DependyAttrubite : Attribute
{
    public DependyAttrubite(DependyLifeTimeEnum lifeTime, Type baseType)
    {
        LifeTime = lifeTime;
        BaseType = baseType;
    }

    public DependyLifeTimeEnum LifeTime { get; set; }

    public Type BaseType { get; set; }
}

public enum DependyLifeTimeEnum
{
    Scoped,
    Singleton,
    Transient
}