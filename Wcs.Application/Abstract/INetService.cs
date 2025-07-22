using Common.Application.Net;

namespace Wcs.Application.Abstract;

public interface INetService
{
    Task<byte[]> ReadAsync();

    Task<bool> WriteAsync();

    Task ReConnect();

    void AddConnect(INet connect);

    void Initialization();
}