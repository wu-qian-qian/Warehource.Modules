using Common.Application.MediatR.Message;
using Plc.Contracts.Input;
using Plc.Contracts.Respon;

namespace Plc.Application.S7ReadWriteHandler.Read;

/// <summary>
///     上层的 模型
/// </summary>
public class ReadPlcEventCommand : ICommand<IEnumerable<ReadBuffer>>
{
    /// <summary>
    ///     PLC的读取数据结构
    /// </summary>
    public ReadBufferInput[] readBufferInputs;

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

    public bool IsBath { get; set; }

    public Guid? Id { get; set; }

    public bool IsApi { get; set; } = false;
}