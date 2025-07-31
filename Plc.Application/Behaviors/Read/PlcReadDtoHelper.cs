using Common.Helper;
using Plc.Contracts.Input;
using Plc.Domain.S7;
using Plc.Shared;

namespace Plc.Application.Behaviors;

/// <summary>
/// 读取模型帮助类
/// </summary>
public static class PlcReadDtoHelper
{
    internal static Dictionary<string, IEnumerable<ReadBufferInput>> _readBufferInputs;
    internal static Semaphore _semaphore = new(1, 1);

    /// <summary>
    /// 初始化读取模型并保存，后续直接使用模型
    /// 后续使用内存，但是有锁(但是只会使用一次锁)
    /// </summary>
    /// <param name="netConfig"></param>
    internal static void UseMemoryInitBufferInput(S7NetConfig netConfig,string key)
    {
        _semaphore.WaitOne();
        try
        {
            if (_readBufferInputs == null) _readBufferInputs = new Dictionary<string, IEnumerable<ReadBufferInput>>();
            if (!_readBufferInputs.ContainsKey(netConfig.Ip))
            {
                List<ReadBufferInput> readBufferInputs = new();
                CreatBufferInput(netConfig,readBufferInputs);
                _readBufferInputs.Add(key, readBufferInputs);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    /// <summary>
    /// 直接初始化模型
    /// 访问数据库频繁，但是无锁
    /// </summary>
    /// <param name="netConfig"></param>
    /// <returns></returns>
    internal static IEnumerable<ReadBufferInput> InitBufferInput(S7NetConfig netConfig)
    {
        var readBufferInputs = new List<ReadBufferInput>();
        CreatBufferInput(netConfig, readBufferInputs);
        return readBufferInputs.ToArray();
    }

    /// <summary>
    /// 创建模型
    /// </summary>
    /// <param name="netConfig"></param>
    /// <param name="readBufferInputs"></param>
    internal static void CreatBufferInput(S7NetConfig netConfig, List<ReadBufferInput> readBufferInputs)
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
                HashId = entityGroup.Key.DeviceName.GetHashCode()
            };
            if (maxEntity.S7DataType != S7DataTypeEnum.Array && maxEntity.S7DataType != S7DataTypeEnum.String
                                                             && maxEntity.S7DataType != S7DataTypeEnum.S7String)
            {
                input.DBEnd = maxEntity.DataOffset +
                              maxEntity.S7DataType.GetEnumAttribute<S7DataTypeAttribute>().DataSize;
            }
            else
            {
                input.DBEnd = maxEntity.DataOffset +
                              maxEntity.ArrtypeLength.Value;
            }

            readBufferInputs.Add(input);
        }
    }
}