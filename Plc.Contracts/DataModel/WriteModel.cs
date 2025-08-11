using Plc.Contracts.Input;

namespace Plc.Contracts.DataModel;

/// <summary>
///     数据的数据结构
/// </summary>
public struct WriteModel
{
    public string _key;

    public string _ipAddress;

    public WriteBufferItemInput _value;

    public string _dBName;
}