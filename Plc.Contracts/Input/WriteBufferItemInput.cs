using Plc.Shared;

namespace Plc.Contracts.Input;

public class WriteBufferItemInput
{
    public string Value { get; set; }

    /// <summary>
    ///     db地址
    /// </summary>
    public int DBAddress { get; set; }

    /// <summary>
    ///     bit地址
    /// </summary>
    public int? DBBit { get; set; }

    /// <summary>
    ///     起始地址
    /// </summary>
    public int DBStart { get; set; }

    /// <summary>
    ///     缓冲区
    /// </summary>
    public byte[]? Buffer { get; set; }

    /// <summary>
    ///     数组长度
    /// </summary>
    public int? ArratCount { get; set; }

    public S7BlockTypeEnum S7BlockType { get; set; }

    public S7DataTypeEnum S7DataType { get; set; }
}