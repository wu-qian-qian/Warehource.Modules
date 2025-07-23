namespace Plc.Shared;

/// <summary>
///     PLC数据块
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