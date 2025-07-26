using Common.Helper;
using Plc.Contracts.Input;
using Plc.Domain.S7;
using Plc.Shared;

namespace Plc.Application.Net.Behaviors;

public static class MemoryReadPlc
{
    internal static Dictionary<string, IEnumerable<ReadBufferInput>> _readBufferInputs;
    internal static Semaphore _semaphore = new(1, 1);

    internal static void InitBufferInput(S7NetConfig netConfig)
    {
        _semaphore.WaitOne();
        try
        {
            if (_readBufferInputs == null) _readBufferInputs = new Dictionary<string, IEnumerable<ReadBufferInput>>();
            if (!_readBufferInputs.ContainsKey(netConfig.Ip))
            {
                List<ReadBufferInput> readBufferInputs = new();
                //对同一ip下的db块与db地址进行分组
                var entityGroups = netConfig.S7EntityItems
                    .GroupBy(p => new { p.S7BlockType, p.DBAddress, p.DeviceName }).ToArray();
                foreach (var entityGroup in entityGroups)
                {
                    var entitys = entityGroup.OrderBy(p => p.Index).ToArray();
                    var start = entitys.Min(p => p.Index);
                    var end = entitys.Max(p => p.Index);
                    var input = new ReadBufferInput
                    {
                        Ip = netConfig.Ip,
                        S7BlockType = entityGroup.Key.S7BlockType,
                        DBStart = start,
                        DBEnd = end,
                        DBAddress = entityGroup.Key.DBAddress,
                        HashId = entityGroup.Key.DeviceName.GetHashCode()
                    };
                    readBufferInputs.Add(input);
                }

                _readBufferInputs.Add(netConfig.Ip, readBufferInputs);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }


    internal static IEnumerable<ReadBufferInput> GetBufferInput(S7NetConfig netConfig)
    {
        List<ReadBufferInput> readBufferInputs = new();
        //对同一ip下的db块与db地址进行分组
        var entityGroups = netConfig.S7EntityItems
            .GroupBy(p => new { p.S7BlockType, p.DBAddress, p.DeviceName }).ToArray();
        foreach (var entityGroup in entityGroups)
        {
            var entitys = entityGroup.OrderBy(p => p.Index).ToArray();
            var start = entitys.Min(p => p.Index);
            var entity = entitys.MaxBy(p => p.Index);
            var input = new ReadBufferInput
            {
                Ip = netConfig.Ip,
                S7BlockType = entityGroup.Key.S7BlockType,
                DBStart = start,
                DBEnd = entity.DataOffset + entity.S7DataType.GetEnumAttribute<S7DataTypeAttribute>().DataSize,
                DBAddress = entityGroup.Key.DBAddress,
                HashId = entityGroup.Key.DeviceName.GetHashCode()
            };
            readBufferInputs.Add(input);
        }

        return readBufferInputs.ToArray();
    }
}