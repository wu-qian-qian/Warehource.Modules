using System.Reflection;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device.Abstract;

namespace Wcs.Device.Helper;

public class CreatDBEntity
{
    private static readonly Dictionary<string, PropertyInfo[]> _properMap;

    static CreatDBEntity()
    {
        _properMap = new Dictionary<string, PropertyInfo[]>();
    }

    public static T CreatEntity<T>(PlcBuffer[] plcBuffers) where T : BaseDBEntity, new()
    {
        var type = typeof(T);
        var t = new T();
        _properMap.TryGetValue(type.Name, out var propers);
        if (propers == null)
        {
            propers = type.GetProperties();
            _properMap[type.Name] = propers;
        }

        for (var i = 0; i < plcBuffers.Length; i++)
        {
            var propertyInfo = propers.First(p => p.Name == plcBuffers[i].DBName);
            propertyInfo.SetValue(t, plcBuffers[i].Data);
        }

        return t;
    }

    public static T CreatEntity<T>(PlcBuffer[] plcBuffers, BaseDBEntity ins) where T : BaseDBEntity, new()
    {
        if (ins == null) ins = new T();

        var type = typeof(T);
        _properMap.TryGetValue(type.Name, out var propers);
        if (propers == null)
        {
            propers = type.GetProperties();
            _properMap[type.Name] = propers;
        }

        for (var i = 0; i < plcBuffers.Length; i++)
        {
            var propertyInfo = propers.First(p => p.Name == plcBuffers[i].DBName);
            propertyInfo.SetValue(ins, plcBuffers[i].Data);
        }

        ins.IsRead = true;
        return (T)ins;
    }
}