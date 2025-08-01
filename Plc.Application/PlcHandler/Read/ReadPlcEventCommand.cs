using Common.Application.MediatR.Message;
using Plc.Contracts.Input;

namespace Plc.Application.PlcHandler.Read;

/// <summary>
///     上层的 模型
/// </summary>
public class ReadPlcEventCommand : ICommand<byte[]>
{
    /// <summary>
    ///     PLC的读取数据结构
    /// </summary>
    public IEnumerable<ReadBufferInput> readBufferInputs;

    public string? Ip { get; set; }

    /// <summary>
    ///     设备名
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    ///     变量名
    /// </summary>
    public IEnumerable<string> DBNames { get; set; }

    /// <summary>
    ///     是否使用内存缓存
    ///     可以减少对数据库的访问
    /// </summary>
    public bool UseMemory { get; set; }

    public bool IsBath { get; set; } = true;
    
    public Guid? Id { get; set; }
}