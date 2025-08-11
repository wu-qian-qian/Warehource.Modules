using Plc.Shared;

namespace Plc.Contracts.Input;

/// <summary>
///     TODO 都是批处理待进行优化成支持单个
/// </summary>
public struct ReadBufferInput
{
    public string Ip { get; set; }

    public int DBAddress { get; set; }

    public int DBStart { get; set; }

    public int DBEnd { get; set; }

    public byte? DBBit { get; set; }

    /// <summary>
    ///     2的16次方
    /// </summary>

    public ushort? ArratCount { get; set; }

    public S7DataTypeEnum S7DataType { get; set; }

    public S7BlockTypeEnum S7BlockType { get; set; }

    /// <summary>
    ///     设备的唯一标识符
    /// </summary>
    public string HashId { get; set; }
}