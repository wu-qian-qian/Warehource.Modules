using Common.Application.MediatR.Message;
using Plc.Contracts.Input;

namespace Plc.Application.S7ReadWriteHandler.Write;

/// <summary>
///     上层的 模型
/// </summary>
public class WritePlcEventCommand : ICommand
{
    /// <summary>
    ///     PLC的读取数据结构
    /// </summary>
    public IEnumerable<WriteBufferInput> writeBufferInputs;

    /// <summary>
    ///     设备名
    ///     如果用缓存必备
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    ///     变量名
    /// </summary>
    public Dictionary<string, string> DBNameToDataValue { get; set; }

    public bool UseMemory { get; set; } = true;
}