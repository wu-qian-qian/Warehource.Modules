namespace Wcs.Shared;

/// <summary>
///     PLC������
/// </summary>
public enum S7TypeEnum
{
    S71200,
    S71500
}

/// <summary>
///     PLC����������
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

/// <summary>
///     PLC�Ŀ�����
/// </summary>
public enum S7BlockTypeEnum
{
    /// <summary>
    ///     Data block memory (DB1, DB2, ...)
    /// </summary>
    DataBlock = 1,

    /// <summary>
    ///     Input area memory (I0.0, I0.1, ...)
    /// </summary>
    Input = 2,

    /// <summary>
    ///     Output area memory (Q0.0, Q0.1, ...)
    /// </summary>
    Output = 3,

    /// <summary>
    ///     Flag area memory (M0.0, M0.1, ...)
    /// </summary>
    Flag = 4,

    /// <summary>
    ///     Timer area memory (T1, T2, ...)
    /// </summary>
    Timer = 5,

    /// <summary>
    ///     Counter area memory (C1, C2, ...)
    /// </summary>
    Counter = 28
}