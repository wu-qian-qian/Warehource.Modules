using Plc.Shared;

namespace Plc.Presentation.S7Plc;

internal record S7NetEntityItemRequest
{
    /// <summary>
    ///     所属的地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    ///     PLC的的数据类型
    /// </summary>
    public S7DataTypeEnum S7DataType { get; set; }

    /// <summary>
    ///     PLC的byte的偏移量
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    ///     bit的偏移量
    /// </summary>
    public int? BitOffset { get; set; }

    /// <summary>
    ///     PLC的类型
    /// </summary>
    public S7BlockTypeEnum S7BlockType { get; set; }

    /// <summary>
    ///     PLC索引
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    ///     字段名称
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     字段名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     数组类型的长度
    /// </summary>
    public int? ArrtypeLength { get; set; }
}