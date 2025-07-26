using Common.Application.Net;
using MediatR;
using Plc.Contracts.Input;

namespace Plc.Application.Abstract;

public interface INetService
{
    public Task<byte[]> ReadAsync(ReadBufferInput input);

    Task<bool> WriteAsync();

    Task ReConnect();

    void AddConnect(INet connect);

    void Initialization(ISender sender);
}