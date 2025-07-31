namespace Wcs.Application.SignalR;

public interface IHubManager
{
    public Task SendUserMessage(string userName, string content);
    public Task SendAllUserMessage(string content);
}