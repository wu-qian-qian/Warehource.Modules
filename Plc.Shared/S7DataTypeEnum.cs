namespace Plc.Shared;

/// <summary>
///     PLC数据类型
/// </summary>
public enum S7DataTypeEnum
{
    [S7DataType(1)] Bool,
    [S7DataType(1)] Byte,
    [S7DataType(2)] Word,
    [S7DataType(4)] DWord,
    [S7DataType(2)] Int,
    [S7DataType(4)] DInt,
    [S7DataType(4)] Real,
    [S7DataType(8)] LReal,
    [S7DataType(32)] String,
    [S7DataType(32)] S7String,
    [S7DataType(32)] Array
}