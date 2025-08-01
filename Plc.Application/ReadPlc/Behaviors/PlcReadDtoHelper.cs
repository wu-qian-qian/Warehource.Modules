using Common.Helper;
using Plc.Contracts.Input;
using Plc.Domain.S7;
using Plc.Shared;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     读取模型帮助类
/// </summary>
public static class PlcReadDtoHelper
{
    internal static Dictionary<string, IEnumerable<ReadBufferInput>> _readBufferInputs= 
        new Dictionary<string, IEnumerable<ReadBufferInput>>();
    internal static Semaphore _semaphore = new(1, 1);

    /// <summary>
    ///     初始化读取模型并保存，后续直接使用模型
    ///     后续使用内存，但是有锁(但是只会使用一次锁)
    /// </summary>
    /// <param name="netConfig"></param>
    internal static void UseMemoryInitBufferInput(S7NetConfig netConfig, string key)
    {
        _semaphore.WaitOne();
        try
        {
            if (!_readBufferInputs.ContainsKey(netConfig.Ip))
            {
                List<ReadBufferInput> readBufferInputs = new();
                CreatBufferInput(netConfig, readBufferInputs);
                _readBufferInputs.Add(key, readBufferInputs);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    internal static void UseMemoryInitBufferInput(string key,params S7EntityItem[] items)
    {
        _semaphore.WaitOne();
        try
        {
            if (!_readBufferInputs.ContainsKey(key))
            {
                var readInputs=  CreatBufferInput(items);
                _readBufferInputs.Add(key,readInputs);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    ///     直接初始化模型
    ///     访问数据库频繁，但是无锁
    /// </summary>
    /// <param name="netConfig"></param>
    /// <returns></returns>
    internal static IEnumerable<ReadBufferInput> InitBufferInput(params S7NetConfig[] netConfigs)
    {
        var readBufferInputs = new List<ReadBufferInput>();
        foreach (var item in netConfigs)
        {
            CreatBufferInput(item, readBufferInputs);
        }
        return readBufferInputs.ToArray();
    }

    /// <summary>
    ///     创建模型
    /// </summary>
    /// <param name="netConfig"></param>
    /// <param name="readBufferInputs"></param>
    private static void CreatBufferInput(S7NetConfig netConfig, List<ReadBufferInput> readBufferInputs)
    {
        //对同一ip下的db块与db地址进行分组
        var entityGroups = netConfig.S7EntityItems
            .GroupBy(p => new { p.S7BlockType, p.DBAddress, p.DeviceName }).ToArray();
        foreach (var entityGroup in entityGroups)
        {
            var entitys = entityGroup.OrderBy(p => p.Index).ToArray();
            var start = entitys.Min(p => p.Index);
            var maxEntity = entitys.MaxBy(p => p.Index);
            var input = new ReadBufferInput
            {
                Ip = netConfig.Ip,
                S7BlockType = entityGroup.Key.S7BlockType,
                DBStart = start,
                DBAddress = entityGroup.Key.DBAddress,
                HashId = entityGroup.Key.DeviceName
            };
            if (maxEntity.S7DataType != S7DataTypeEnum.Array && maxEntity.S7DataType != S7DataTypeEnum.String
                                                             && maxEntity.S7DataType != S7DataTypeEnum.S7String)
                input.DBEnd = maxEntity.DataOffset +
                              maxEntity.S7DataType.GetEnumAttribute<S7DataTypeAttribute>().DataSize;
            else
                input.DBEnd = maxEntity.DataOffset +
                              maxEntity.ArrtypeLength.Value;

            readBufferInputs.Add(input);
        }
    }
    
    
    /// <summary>
    /// S7EntityItem 初始化读取模型
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    internal static  List<ReadBufferInput> CreatBufferInput(params S7EntityItem[] items)
    {
        var readBufferInputs = new List<ReadBufferInput>();
        var itemsGroup = items.GroupBy(p => p.Ip);
        foreach (var itemGroup in itemsGroup)
        {
            foreach (var item in itemGroup)
            {
                var input = new ReadBufferInput
                {
                    Ip = itemGroup.Key,
                    S7BlockType = item.S7BlockType,
                    DBStart = item.DataOffset,
                    DBAddress = item.DBAddress,
                    HashId =item.Name
                };
                if (item.S7DataType != S7DataTypeEnum.Array && item.S7DataType != S7DataTypeEnum.String
                                                            && item.S7DataType != S7DataTypeEnum.S7String)
                    input.DBEnd = item.DataOffset +
                                  item.S7DataType.GetEnumAttribute<S7DataTypeAttribute>().DataSize;
                else
                    input.DBEnd = item.DataOffset +
                                  item.ArrtypeLength.Value;

                readBufferInputs.Add(input);
            }
        }
        return readBufferInputs;
    }
}