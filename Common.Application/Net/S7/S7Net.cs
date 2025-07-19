using S7.Net;

namespace Common.Application.Net.S7;

public abstract class S7Net : INet
{
    public Plc _plc { get; protected set; }

    public abstract void Connect();

    public abstract void ReConnect();

    public abstract void Close();
}