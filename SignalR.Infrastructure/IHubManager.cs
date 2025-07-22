namespace SignalR.Application;

public interface IHubManager
{
    public Task SendUserMessage(string userName, string content);
    public Task SendAllUserMessage(string content);
}