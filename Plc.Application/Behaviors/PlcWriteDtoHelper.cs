using Plc.Contracts.Input;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors;

internal static partial class PlcReadWriteDtoHelper
{
    internal static List<WriteKey> _WriteBufferInputs = new();

    internal static Semaphore _semaphoreWrite = new(1, 1);

    internal static void UseMemoryInitWriteBufferInput(string key, params S7EntityItem[] s7EntityItems)
    {
        _semaphoreWrite.WaitOne();
        try
        {
            if (!_WriteBufferInputs.Any(p => p._key == key))
            {
                var S7EntityItemGroup = s7EntityItems.GroupBy(p => p.Ip);
                foreach (var groupItem in S7EntityItemGroup)
                foreach (var item in groupItem)
                {
                    var writeKey = new WriteKey();
                    writeKey._key = key;
                    writeKey._ipAddress = groupItem.Key;
                    var itemInput = new WriteBufferItemInput
                    {
                        ArratCount = item.ArrtypeLength,
                        DBBit = item.BitOffset,
                        DBAddress = item.DBAddress,
                        DBStart = item.DataOffset,
                        S7BlockType = item.S7BlockType,
                        S7DataType = item.S7DataType
                    };
                    writeKey._value = itemInput;
                    writeKey._dBName = item.Name;
                }
            }
        }
        finally
        {
            _semaphoreWrite.Release();
        }
    }


    internal static List<WriteBufferItemInput> CreatWriteBufferInput(params S7EntityItem[] s7EntityItems)
    {
        var S7EntityItemGroup = s7EntityItems.GroupBy(p => p.Ip);
        var itemInputs = new List<WriteBufferItemInput>();
        foreach (var groupItem in S7EntityItemGroup)
        foreach (var item in groupItem)
        {
            var itemInput = new WriteBufferItemInput
            {
                ArratCount = item.ArrtypeLength,
                DBBit = item.BitOffset,
                DBAddress = item.DBAddress,
                DBStart = item.DataOffset,
                S7BlockType = item.S7BlockType,
                S7DataType = item.S7DataType
            };
            itemInputs.Add(itemInput);
        }

        return itemInputs;
    }
}