using Plc.Contracts.Input;
using Plc.Infrastructure.Helper;
using S7.Net.Types;

namespace Plc.Infrastructure.Token;

public partial class S7NetToken
{
    public static DataItem CreatReadS7Item(ReadBufferInput input)
    {
        var dbType = EnumConvert.S7BlockTypeToDataType(input.S7BlockType);
        var bulkItem = new DataItem
        {
            DB = input.DBAddress,
            StartByteAdr = input.DBStart,
            BitAdr =input.DBBit??0,
            Count = input.DBEnd - input.DBStart,
            DataType = dbType
        };
        return bulkItem;
    }
}