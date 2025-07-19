using System.Diagnostics;
using System.Reflection;

namespace Common.Helper;

public static class MethodHelper
{
    /// <summary>
    ///     获取方式上面的特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public static T GetMethodDescription<T>(this object service) where T : Attribute
    {
        var attr = default(T);
        var typeInfo = service.GetType();
        var frames = new StackTrace().GetFrames();
        var frame = frames?.FirstOrDefault(x => x.GetMethod().ReflectedType == typeInfo);
        var methodInfo = frame?.GetMethod() as MethodInfo;
        attr = methodInfo?.GetCustomAttribute<T>();
        if (attr == null)
        {
            var assembly = typeInfo.Assembly;
            var type = assembly.GetType($"{typeInfo.Namespace}.{typeInfo.Name}");
            var lastMethodName = GetLastMethodName(methodInfo.Name);
            var tempMethodInfo = type.GetMethod(lastMethodName);
            attr = tempMethodInfo?.GetCustomAttribute<T>();
        }

        return attr;
    }


    private static string GetLastMethodName(string methodName)
    {
        if (methodName.Contains("_"))
        {
            var tempList = methodName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList();
            tempList.RemoveRange(1, 2);
            return string.Join("_", tempList);
        }

        return methodName;
    }
}