using Common.Application.Net;
using Plc.Contracts.Input;
using S7.Net.Types;

namespace Plc.Application.Net;

public abstract class S7Net : INet
{
    public global::S7.Net.Plc _plc { get; protected set; }

    public abstract void Connect();

    public abstract void ReConnect();
    public abstract void Close();

    public abstract Task<byte[]> ReadAsync(ReadBufferInput input);

    public abstract Task<bool> WriteAsync(WriteBufferInput[] bulkItem);
}