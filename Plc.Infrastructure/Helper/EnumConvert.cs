using Plc.Shared;
using S7.Net;

namespace Plc.Infrastructure.Helper;

public static class EnumConvert
{
    public static DataType S7BlockTypeToDataType(S7BlockTypeEnum @enum)
    {
        var dbType = @enum switch
        {
            S7BlockTypeEnum.DataBlock => DataType.DataBlock,
            S7BlockTypeEnum.Memory => DataType.Memory,
            S7BlockTypeEnum.Input => DataType.Input,
            _ => throw new AggregateException("无法解析")
        };
        return dbType;
    }

    public static VarType S7DataTypeToVarType(S7DataTypeEnum @enum)
    {
        var dbType = @enum switch
        {
            S7DataTypeEnum.Bool => VarType.Bit,
            S7DataTypeEnum.Byte => VarType.Byte,
            S7DataTypeEnum.Word => VarType.Word,
            S7DataTypeEnum.DWord => VarType.DWord,
            S7DataTypeEnum.Int => VarType.Int,
            S7DataTypeEnum.DInt => VarType.DInt,
            S7DataTypeEnum.Real => VarType.Real,
            S7DataTypeEnum.LReal => VarType.LReal,
            S7DataTypeEnum.String => VarType.String,
            S7DataTypeEnum.S7String => VarType.S7String,
            _ => throw new AggregateException("无法解析")
        };
        return dbType;
    }
}