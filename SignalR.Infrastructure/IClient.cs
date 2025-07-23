namespace SignalR.Application;

/// <summary>
///     IClient 接口定义了 SignalR 客户端的基本行为。
/// </summary>
public interface IClient
{
    public Task SendMessage(string message);
}