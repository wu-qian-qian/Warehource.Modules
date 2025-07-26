using System.ComponentModel;
using System.Reflection;

namespace Common.Helper;

public static class EnumHelper
{
    /// <summary>
    ///     获取 枚举值的注释
    /// </summary>
    /// <param name="thisEnum"></param>
    /// <returns></returns>
    public static string? GetEnumDescription(this Enum thisEnum)
    {
        var descriptionAttribute = thisEnum.GetEnumAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description;
    }


    /// <summary>
    ///     获取对应 枚举值特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="thisEnum"></param>
    /// <returns></returns>
    public static T GetEnumAttribute<T>(this Enum thisEnum) where T : Attribute
    {
        var type = thisEnum.GetType();
        var fieldInfo = type.GetField(thisEnum.ToString());
        if (fieldInfo != null)
        {
            object attrObject = fieldInfo.GetCustomAttribute(typeof(T));
            return attrObject as T;
        }

        return null;
    }

    public static Enum TryGetEnum(string enumName, Type enumType)
    {
        if (enumType.IsEnum)
        {
            var enumValue = Enum.Parse(enumType, enumName, true);
            return (Enum)enumValue;
        }

        throw new ArgumentException($"Type {enumType.Name} is not an enum type.");
    }
}