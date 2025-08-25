using Common.Helper;
using Plc.Contracts.Input;
using Plc.Domain.S7;
using Plc.Shared;

namespace Plc.Application.Behaviors;

/// <summary>
///     读取模型帮助类
/// </summary>
internal static partial class PlcReadWriteDtoHelper
{
    internal static Dictionary<string, IEnumerable<ReadBufferInput>> _readBufferInputs = new();

    internal static SemaphoreSlim _semaphoreSlimRead = new(1, 1);

    /// <summary>
    ///     初始化读取模型并保存，后续直接使用模型
    ///     后续使用内存，但是有锁(但是只会使用一次锁)
    /// </summary>
    /// <param name="netConfig"></param>
    internal static void UseMemoryInitReadBufferInput(S7NetConfig netConfig, string key)
    {
        _semaphoreSlimRead.Wait();
        try
        {
            if (!_readBufferInputs.ContainsKey(netConfig.Ip))
            {
                List<ReadBufferInput> readBufferInputs = new();
                CreatReadBufferInput(netConfig, readBufferInputs);
                _readBufferInputs.Add(key, readBufferInputs.ToArray());
            }
        }
        finally
        {
            _semaphoreSlimRead.Release();
        }
    }

    /// <summary>
    ///     创建模型
    /// </summary>
    /// <param name="netConfig"></param>
    /// <param name="readBufferInputs"></param>
    internal static void CreatReadBufferInput(S7NetConfig netConfig, List<ReadBufferInput> readBufferInputs)
    {
        //对同一ip下的db块与db地址进行分组
        var entityGroups = netConfig.S7EntityItems
            .GroupBy(p => new { p.S7BlockType, p.DBAddress, p.DeviceName }).ToArray();
        foreach (var entityGroup in entityGroups)
        {
            var entitys = entityGroup.OrderBy(p => p.Index).ToArray();
            var start = entitys.Min(p => p.DataOffset);
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
                              maxEntity.ArrayLength.Value;

            readBufferInputs.Add(input);
        }
    }


    internal static void UseMemoryInitReadBufferInput(string key, params S7EntityItem[] items)
    {
        _semaphoreSlimRead.Wait();
        try
        {
            if (!_readBufferInputs.ContainsKey(key))
            {
                var readInputs = CreatReadBufferInput(items);
                _readBufferInputs.Add(key, readInputs);
            }
        }
        finally
        {
            _semaphoreSlimRead.Release();
        }
    }

    /// <summary>
    ///     S7EntityItem 初始化读取模型
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    internal static List<ReadBufferInput> CreatReadBufferInput(params S7EntityItem[] items)
    {
        var readBufferInputs = new List<ReadBufferInput>();
        var itemsGroup =
            items.GroupBy(p => new { p.S7BlockType, p.Ip, p.DBAddress });
        foreach (var itemGroup in itemsGroup)
        {
            var entitys = itemGroup.OrderBy(p => p.Index).ToArray();
            var start = entitys.Min(p => p.DataOffset);
            var maxEntity = entitys.MaxBy(p => p.Index);
            var input = new ReadBufferInput
            {
                Ip = itemGroup.Key.Ip,
                S7BlockType = itemGroup.Key.S7BlockType,
                DBStart = start,
                DBAddress = itemGroup.Key.DBAddress,
                HashId = entitys.First().DeviceName
            };
            if (maxEntity.S7DataType != S7DataTypeEnum.Array && maxEntity.S7DataType != S7DataTypeEnum.String
                                                             && maxEntity.S7DataType != S7DataTypeEnum.S7String)
                input.DBEnd = maxEntity.DataOffset +
                              maxEntity.S7DataType.GetEnumAttribute<S7DataTypeAttribute>().DataSize;
            else
                input.DBEnd = maxEntity.DataOffset +
                              maxEntity.ArrayLength.Value;

            readBufferInputs.Add(input);
        }

        return readBufferInputs;
    }
}