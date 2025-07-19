using Common.Application.MediatR.Message;

namespace Wcs.Application.S7Plc.ReadPlcCommand;

/// <summary>
///     PLC 读取数据模型
/// </summary>
public sealed class ReadPlcEvent : PlcEventCommand
{
}

/// <summary>
///     上层的 模型
/// </summary>
public class PlcEventCommand : ICommand<byte[]>
{
    public string Ip { get; set; }
}