using S7.Net.Types;

namespace Common.Application.Net.S7;

public abstract class S7Net : INet
{
    public global::S7.Net.Plc _plc { get; protected set; }

    public abstract void Connect();

    public abstract void ReConnect();

    public abstract Task<byte[]> ReadAsync(DataItem bulkItem);
    public abstract void Close();
}