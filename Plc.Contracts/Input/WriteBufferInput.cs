namespace Plc.Contracts.Input;

/// <summary>
///     考虑对象池管理
/// </summary>
public class WriteBufferInput
{
    public string Ip { get; set; }

    public string Device { get; set; }

    public WriteBufferItemInput[] WriteBufferItemArray { get; set; }
}