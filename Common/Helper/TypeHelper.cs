namespace Common.Helper;

public static class TypeHelper
{
    /// <summary>
    ///     值类型的值转换为 Nullable 类型的值
    /// </summary>
    /// <param name="datatype"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static object NullableSetValue(Type datatype, string value)
    {
        if (datatype.IsGenericType && datatype.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(datatype);
            var convertedValue = Convert.ChangeType(value, underlyingType);
            return convertedValue;
        }
        else
        {
            var convertedValue = Convert.ChangeType(value, datatype);
            return convertedValue;
        }
    }

    public static bool IsNullableType(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}