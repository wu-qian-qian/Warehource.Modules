using Common.Application.Net;
using MediatR;

namespace Plc.Application.Abstract;

public interface INetService
{
    Task<byte[]> ReadAsync();

    Task<bool> WriteAsync();

    Task ReConnect();

    void AddConnect(INet connect);

    void Initialization(ISender sender);
}