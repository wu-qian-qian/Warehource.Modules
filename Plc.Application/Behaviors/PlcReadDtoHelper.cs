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

    /// <summary>
    /// 缓存数据重新加载
    /// </summary>
    /// <returns></returns>
    internal static Exception ReloadRead()
    {
        Exception exception = null;
        try
        {
            _semaphoreSlimRead.Wait();
            _readBufferInputs.Clear();
        }
        catch (Exception ex)
        {
            exception = ex;
        }
        finally
        {
            _semaphoreSlimRead.Release();
        }

        return exception;
    }
}