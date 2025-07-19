namespace Common.Application.Net;

public interface INetService
{
    Task<byte[]> ReadAsync();

    Task<bool> WriteAsync();

    Task ReConnect();

    void AddConnect(INet connect);
}