using Common.Application.MediatR.Message;
using Plc.Contracts.Input;

namespace Plc.Application.Net.ReadPlc;

/// <summary>
///     上层的 模型
/// </summary>
public class PlcEventCommand : ICommand
{
    /// <summary>
    ///     PLC的读取数据结构
    /// </summary>
    public IEnumerable<ReadBufferInput> readBufferInputs;

    public string? Ip { get; set; }

    public string? DeviceName { get; set; }

    /// <summary>
    ///     是否使用内存缓存
    ///     可以减少对数据库的访问
    /// </summary>
    public bool UseMemory { get; set; }
}