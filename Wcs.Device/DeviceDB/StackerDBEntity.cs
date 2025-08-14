using System.Reflection;
using Wcs.Contracts.Respon.Plc;

namespace Wcs.Device.DeviceDB;

public class StackerDBEntity : BaseEntity
{
    public static PropertyInfo[] _propertyInfos = typeof(StackerDBEntity).GetProperties();
    public string RTask { get; set; }

    public static StackerDBEntity CreatDBEntity(PlcBuffer[] plcBuffers)
    {
        var entity = new StackerDBEntity();
        for (var i = 0; i < plcBuffers.Length; i++)
        {
            var propertyInfo = _propertyInfos.First(p => p.Name == plcBuffers[i].DBName);
            propertyInfo.SetValue(entity, plcBuffers[i].Data);
        }

        return entity;
    }
}