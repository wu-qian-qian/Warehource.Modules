using System.Reflection;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device.Abstract;

namespace Wcs.Device.Helper;

public class CreatDBEntity
{
    private static readonly Dictionary<string, PropertyInfo[]?> ProperMap;

    private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);

    static CreatDBEntity()
    {
        ProperMap = new Dictionary<string, PropertyInfo[]?>();
    }

    public static T CreatEntity<T>(PlcBuffer[] plcBuffers) where T : BaseDBEntity, new()
    {
        var type = typeof(T);
        var t = new T();
        PropertyInfo[]? propers = default;
        try
        {
            ProperMap.TryGetValue(type.Name, out propers);
            //双重校验
            if (propers == null)
            {
                SemaphoreSlim.Wait();
                ProperMap.TryGetValue(type.Name, out propers);
                if (propers == null)
                {
                    propers = type.GetProperties();
                    ProperMap[type.Name] = propers;
                }
            }
        }
        finally
        {
            SemaphoreSlim.Release();
        }

        for (var i = 0; i < plcBuffers.Length; i++)
        {
            var propertyInfo = propers.First(p => p.Name == plcBuffers[i].DBName);
            propertyInfo.SetValue(t, plcBuffers[i].Data);
        }

        return t;
    }

    /// <summary>
    ///     可能需要考虑添加锁
    /// </summary>
    /// <param name="plcBuffers"></param>
    /// <param name="ins"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreatEntity<T>(PlcBuffer[] plcBuffers, BaseDBEntity? ins) where T : BaseDBEntity, new()
    {
        if (ins == null)
            ins = new T();

        var type = ins.GetType();
        PropertyInfo[]? propers = default;
        try
        {
            ProperMap.TryGetValue(type.Name, out propers);
            //双重校验
            if (propers == null)
            {
                SemaphoreSlim.Wait();
                ProperMap.TryGetValue(type.Name, out propers);
                if (propers == null)
                {
                    propers = type.GetProperties();
                    ProperMap[type.Name] = propers;
                }
            }
        }
        finally
        {
            SemaphoreSlim.Release();
        }

        for (var i = 0; i < plcBuffers.Length; i++)
        {
            var propertyInfo = propers.First(p => p.Name == plcBuffers[i].DBName);
            propertyInfo.SetValue(ins, plcBuffers[i].Data);
        }

        return (T)ins;
    }
}