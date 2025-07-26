using Common.Application.Net;
using MediatR;
using Plc.Contracts.Input;

namespace Plc.Application.Abstract;

public interface INetService
{
    Task<byte[]> ReadAsync(ReadBufferInput input);

    Task<bool> WriteAsync(WriteBufferInput input);

    Task ReConnect();

    void AddConnect(INet connect);

    void Initialization(ISender sender);
}