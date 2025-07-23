using Common.Application.MediatR.Message;

namespace Plc.Application.S7Plc.ReadPlc;

/// <summary>
///     上层的 模型
/// </summary>
public class PlcEventCommand : ICommand<byte[]>
{
    public string Ip { get; set; }
}