using Plc.Contracts.Input;

namespace Plc.Application.Behaviors;

internal struct WriteKey
{
    public string _key;

    public string _ipAddress;

    public WriteBufferItemInput _value;

    public string _dBName;
}